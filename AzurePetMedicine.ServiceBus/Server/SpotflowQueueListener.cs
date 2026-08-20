using Azure.Messaging.ServiceBus;
using AzurePetMedicine.ServiceBus.Server.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AzurePetMedicine.ServiceBus.Server
{
    public class SpotflowQueueListener : BackgroundService
    {
        private readonly ServiceBusClient _sbClient;
        private readonly IHubContext<MessageHub> _hubContext;
        private readonly IConfiguration _configuration;
        private readonly ILogger<SpotflowQueueListener> _logger;
        private readonly List<ServiceBusProcessor> _processors = new();

        public SpotflowQueueListener(
            ServiceBusClient sbClient,
            IHubContext<MessageHub> hubContext,
            IConfiguration configuration,
            ILogger<SpotflowQueueListener> logger)
        {
            _sbClient = sbClient;
            _hubContext = hubContext;
            _configuration = configuration;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var topicsWithSubs = _configuration
                .GetSection("ServiceBus:TopicsWithSubscriptions")
                .Get<Dictionary<string, List<string>>>() ?? new Dictionary<string, List<string>>();

            foreach (var (topicName, subscriptions) in topicsWithSubs)
            {
                if (subscriptions == null) continue;

                foreach (var subName in subscriptions)
                {
                    var processor = _sbClient.CreateProcessor(topicName, subName, new ServiceBusProcessorOptions());

                    var currentTopic = topicName;
                    var currentSub = subName;

                    processor.ProcessMessageAsync += args => OnProcessMessageAsync(args, currentTopic, currentSub, stoppingToken);
                    processor.ProcessErrorAsync += args => OnProcessErrorAsync(args, currentTopic, currentSub);

                    await processor.StartProcessingAsync(stoppingToken);
                    _processors.Add(processor);
                }
            }
        }

        private async Task OnProcessMessageAsync(ProcessMessageEventArgs args, string topicName, string subName, CancellationToken cancellationToken)
        {
            var body = args.Message.Body.ToString();
            _logger.LogInformation("Spotflow processed message ID {Id} for [{Topic}/{Sub}]",
                args.Message.MessageId, topicName, subName);

            var groupName = $"{topicName}/{subName}";
            await _hubContext.Clients.Group(groupName)
                .SendAsync("ReceiveMessage", body, cancellationToken);

            await args.CompleteMessageAsync(args.Message, cancellationToken);
        }

        private Task OnProcessErrorAsync(ProcessErrorEventArgs args, string topicName, string subName)
        {
            _logger.LogError(args.Exception, "Error in processor [{Topic}/{Sub}]", topicName, subName);
            return Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            foreach (var processor in _processors)
            {
                await processor.StopProcessingAsync(cancellationToken);
                await processor.DisposeAsync();
            }
            await base.StopAsync(cancellationToken);
        }
    }
}