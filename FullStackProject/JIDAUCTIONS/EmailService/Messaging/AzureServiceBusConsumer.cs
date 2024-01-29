using Azure.Messaging.ServiceBus;
using EmailService.Models;
using EmailService.Models.Dtos;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;
using EmailService.Services;

namespace EmailService.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string _queueName;
        private readonly ServiceBusProcessor _emailProcessor;
        private readonly ServiceBusProcessor _bookingProcessor;
        private readonly MailService _emailService;
        private readonly EmailsService _email;


        public AzureServiceBusConsumer(IConfiguration configuration, EmailsService service)
        {
            _configuration = configuration;
            _email = service;

            _connectionString = _configuration.GetValue<string>("AzureConnectionString");
            _queueName = _configuration.GetValue<string>("QueueAndTopics:registerQueue");

            var client = new ServiceBusClient(_connectionString);
            _emailProcessor = client.CreateProcessor(_queueName);
            /*_bookingProcessor = client.CreateProcessor("bookingadded", "EmailService");*/
            _emailService = new MailService(configuration);

        }
        public async Task Start()
        {
            _emailProcessor.ProcessMessageAsync += OnRegisterUser;
            _emailProcessor.ProcessErrorAsync += ErrorHandler;
            await _emailProcessor.StartProcessingAsync();

            /*_bookingProcessor.ProcessMessageAsync += OnBooking;
            _bookingProcessor.ProcessErrorAsync += ErrorHandler;
            await _bookingProcessor.StartProcessingAsync();*/
        }

      
        private Task ErrorHandler(ProcessErrorEventArgs arg)
        {
            //send Email to Admin 
            return Task.CompletedTask;
        }

        private async  Task OnRegisterUser(ProcessMessageEventArgs arg)
        {
            var message = arg.Message;
            var body = Encoding.UTF8.GetString(message.Body);//read  as String
            var user = JsonConvert.DeserializeObject<UserMessageDto>(body);//string to UserMessageDto

            try
            {

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<img src=\"https://i.pinimg.com/236x/eb/02/ea/eb02ea9b15eab6b211005334674e5776.jpg\" width =\"1000\" height=\"600\">");
                stringBuilder.Append("<h1> Hello " + user.Name + "</h1>");
                stringBuilder.AppendLine("<br/>Welcome to JIDAUCTIONS, The best auction around!!");

                stringBuilder.Append("<br/>");
                stringBuilder.Append('\n');
                stringBuilder.Append("<p>Have a Look at what we have for you!!</p>");

                await _emailService.sendEmail(user, stringBuilder.ToString());


                //insert  to Database
                var emaiLLogger = new EmailLogger()
                {
                    Name = user.Name,
                    Email = user.Email,
                    Message = stringBuilder.ToString(),
                    DateTime = DateTime.Now,

                };
                await _email.addDatatoDatabase(emaiLLogger);

                await arg.CompleteMessageAsync(arg.Message);//we are done delete the message from the queue 
            }
            catch (Exception ex)
            {
                throw;
                //send an Email to Admin
            }
        }

        public async Task Stop()
        {
            await _emailProcessor.StopProcessingAsync();
            await _emailProcessor.DisposeAsync();

            await _bookingProcessor.StopProcessingAsync();
            await _bookingProcessor.DisposeAsync();
        }
    }
}
