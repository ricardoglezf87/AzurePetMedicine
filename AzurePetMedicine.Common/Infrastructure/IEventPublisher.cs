namespace AzurePetMedicine.ServiceBus.Infrastructure
{
    public interface IEventPublisher
    {
        Task PublishAsync<TEvent>(TEvent @event, string topicOrQueueName) where TEvent : class;

    }
}
