
using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using RewardsService.Models;
using RewardsService.Models.Dto;
using RewardsService.Service;
using System.Text;

namespace RewardsService.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string _topicName;
        private readonly string _subscription;
        private readonly ServiceBusProcessor _rewardsProcessor;
        private readonly RewardService _rewardService;

        public AzureServiceBusConsumer(IConfiguration configuration, RewardService reward)
        {
            _configuration = configuration;
            _rewardService = reward;
            _connectionString = _configuration.GetValue<string>("AzureConnectionString");
            _topicName = _configuration.GetValue<string>("QueueAndTopics:bookingTopic");
            _subscription = _configuration.GetValue<string>("QueueAndTopics:bookingSubscription");

            var client = new ServiceBusClient(_connectionString);
            _rewardsProcessor = client.CreateProcessor(_topicName, _subscription);

        }
        public async Task Start()
        {
            _rewardsProcessor.ProcessMessageAsync += OnOrder;
            _rewardsProcessor.ProcessErrorAsync += ErrorHandler;
            await _rewardsProcessor.StartProcessingAsync();
        }

        public async Task Stop()
        {
            await _rewardsProcessor.StopProcessingAsync();
            await _rewardsProcessor.DisposeAsync();
        }

        private async Task OnOrder(ProcessMessageEventArgs arg)
        {

            var message = arg.Message;
            var body = Encoding.UTF8.GetString(message.Body);
            var reward = JsonConvert.DeserializeObject<RewardsDto>(body);

            try
            {
                //insert  to Database
                var rwd = new Reward()
                {
                    OrderId = reward.OrderId,
                    TotalAmount = reward.TotalAmount,
                    Email = reward.Email,
                    Name = reward.Name,
                    Points = reward.TotalAmount / 1000

                };

                await _rewardService.AddReward(rwd);
                await arg.CompleteMessageAsync(arg.Message);
            }
            catch (Exception ex)
            {
                throw;
                
            }
        }



        private Task ErrorHandler(ProcessErrorEventArgs arg)
        {

            
            return Task.CompletedTask;
        }
    }
}
