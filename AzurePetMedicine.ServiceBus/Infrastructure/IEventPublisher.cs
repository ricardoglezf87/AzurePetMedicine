using System;
using System.Collections.Generic;
using System.Text;

namespace AzurePetMedicine.ServiceBus.Infrastructure
{
    public interface IEventPublisher
    {
        Task PublishAsync<TEvent>(TEvent @event, string topicOrQueueName)
            where TEvent : class;
    }
}
