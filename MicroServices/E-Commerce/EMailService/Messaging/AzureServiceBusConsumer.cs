
using Azure.Messaging.ServiceBus;
using EMailService.Models;
using EMailService.Models.Dtos;
using EMailService.Services;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;


namespace EMailService.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string _queueName;
        private readonly ServiceBusProcessor _emailProcessor;
        private readonly ServiceBusProcessor _bookingProcessor;
        private readonly MailService _emailService;
        private readonly EmailService _email;
        public Task Start()
        {
            throw new NotImplementedException();
        }

        public Task Stop()
        {
            throw new NotImplementedException();
        }
    }
}
