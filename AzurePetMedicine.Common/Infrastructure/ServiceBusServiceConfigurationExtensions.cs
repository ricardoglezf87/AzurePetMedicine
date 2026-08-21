using Microsoft.Extensions.DependencyInjection;

namespace AzurePetMedicine.ServiceBus.Infrastructure
{
    public static class ServiceBusServiceConfigurationExtensions
    {
        public static IServiceCollection AddHttpServiceBusSimulator(
             this IServiceCollection services,
             string simulatorUrl)
        {
            services.AddHttpClient<IEventPublisher, ServiceBusEventPublisher>(client =>
            {
                client.BaseAddress = new Uri(simulatorUrl);
            });

            return services;
        }
    }
}
