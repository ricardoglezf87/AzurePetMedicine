using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Spotflow.InMemory.Azure.ServiceBus;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AzurePetMedicine.ServiceBus.Infrastructure
{
    public static class ServiceBusServiceConfigurationExtensions
    {
        public static IServiceCollection AddInMemoryServiceBus(
        this IServiceCollection services,
        string namespaceName,
        List<string>? topics = null,
        List<string>? queues = null)
        {
            var provider = new InMemoryServiceBusProvider();

            // Configurar el Namespace y crear los Topics/Queues al iniciar
            var ns = provider.AddNamespace(namespaceName);
            if (topics != null)
            {
                foreach (var topic in topics)
                {
                    ns.AddTopic(topic);
                }
            }

            // Crear Queues
            if (queues != null)
            {
                foreach (var queue in queues)
                {
                    ns.AddQueue(queue);
                }
            }

            string connectionString = $"Endpoint=sb://{namespaceName}.servicebus.in-memory.example.com/;SharedAccessKeyName=Root;SharedAccessKey=fake";

            // Registrar el cliente simulado como Singleton
            services.AddSingleton<ServiceBusClient>(new InMemoryServiceBusClient(connectionString, provider));
            services.AddScoped<IEventPublisher, ServiceBusEventPublisher>();

            return services;
        }

        // Opción para Producción / Entornos reales
        public static IServiceCollection AddAzureServiceBus(
            this IServiceCollection services,
            string connectionString)
        {
            services.AddSingleton(new ServiceBusClient(connectionString));
            services.AddScoped<IEventPublisher, ServiceBusEventPublisher>();

            return services;
        }
    }
}
