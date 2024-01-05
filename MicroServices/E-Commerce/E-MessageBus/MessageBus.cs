using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_MessageBus
{
    public class MessageBus:IMessageBus
    {
        private readonly string connectionString = "Endpoint=sb://e-commercebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Ps/LKffD/hqb31nWuLihaf4lqcB/7JbB1+ASbCWWmcw=";

        public async Task PublishMessage(object message, string Topic_Queue_Name)
        {
            //create a client 
            var client = new ServiceBusClient(connectionString);

            ServiceBusSender sender = client.CreateSender(Topic_Queue_Name);

            //convert to Json
            var body = JsonConvert.SerializeObject(message);

            ServiceBusMessage theMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(body))
            {
                CorrelationId = Guid.NewGuid().ToString(),
            };

            //send the message 
            await sender.SendMessageAsync(theMessage);

            //free the Resources/Clean uP
            await sender.DisposeAsync();
        }
    }
}
