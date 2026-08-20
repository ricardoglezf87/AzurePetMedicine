using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

namespace AzurePetMedicine.Rescue.Api.IntegrationEvents
{
    public class PetFlaggerForAdoptionIntegrationEventHandler : BackgroundService
    {
        private readonly ILogger<PetFlaggerForAdoptionIntegrationEventHandler> _logger;
        private readonly HubConnection _hubConnection;
        private readonly IConfiguration _configuration;

        public PetFlaggerForAdoptionIntegrationEventHandler(
            ILogger<PetFlaggerForAdoptionIntegrationEventHandler> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

            _hubConnection = new HubConnectionBuilder()
                .WithUrl(_configuration["serverurl"] + "/messageHub")
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.On<string>("ReceiveMessage", (jsonBody) =>
            {
                var eventData = JsonConvert.DeserializeObject<PetFlaggedForAdoptionIntegrationEvent>(jsonBody);
                _logger.LogInformation("Message received from simulator for pet: {Name}", eventData?.Name);
            });
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _hubConnection.StartAsync(stoppingToken);
            await _hubConnection.InvokeAsync("Subscribe", "adoption-topic", "adoption-rescue", stoppingToken);
            _logger.LogInformation("Successfully subscribed to adoption event channel.");
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _hubConnection.StopAsync(cancellationToken);
            await _hubConnection.DisposeAsync();
            _logger.LogInformation("Disconnected from PetFlaggedForAdoptionIntegrationEvent");
            await base.StopAsync(cancellationToken);
        }
    }
}