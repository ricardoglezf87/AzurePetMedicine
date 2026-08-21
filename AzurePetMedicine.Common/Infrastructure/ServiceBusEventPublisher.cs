using Newtonsoft.Json;
using System.Net.Http.Json;

namespace AzurePetMedicine.ServiceBus.Infrastructure
{
    public class ServiceBusEventPublisher : IEventPublisher
    {
        private readonly HttpClient _httpClient;

        public ServiceBusEventPublisher(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task PublishAsync<TEvent>(TEvent @event, string topicName) where TEvent : class
        {
            var jsonBody = JsonConvert.SerializeObject(@event);

            var dto = new
            {
                Body = jsonBody,
                Subject = typeof(TEvent).FullName
            };

            await _httpClient.PostAsJsonAsync($"api/messages/publish/{topicName}", dto);
        }

    }
}
