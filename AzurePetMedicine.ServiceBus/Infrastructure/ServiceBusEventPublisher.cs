using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;

namespace AzurePetMedicine.ServiceBus.Infrastructure
{
    public class ServiceBusEventPublisher : IEventPublisher
    {
        private readonly ServiceBusClient _client;

        public ServiceBusEventPublisher(ServiceBusClient client)
        {
            _client = client;
        }

        public async Task PublishAsync<TEvent>(TEvent @event, string topicOrQueueName)
            where TEvent : class
        {
            var jsonMessage = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            var sender = _client.CreateSender(topicOrQueueName);

            var message = new ServiceBusMessage(new BinaryData(body))
            {
                MessageId = Guid.NewGuid().ToString(),
                ContentType = MediaTypeNames.Application.Json,
                Subject = typeof(TEvent).FullName
            };

            await sender.SendMessageAsync(message);
        }
    }
}
