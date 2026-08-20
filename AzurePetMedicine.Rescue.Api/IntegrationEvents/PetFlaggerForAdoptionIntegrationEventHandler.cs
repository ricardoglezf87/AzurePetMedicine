using AzurePetMedicine.Common.Domains;
using AzurePetMedicine.Rescue.Api.Infrastructure;
using AzurePetMedicine.Rescue.Domain.Entities;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace AzurePetMedicine.Rescue.Api.IntegrationEvents
{
    public class PetFlaggerForAdoptionIntegrationEventHandler : BackgroundService
    {
        private readonly ILogger<PetFlaggerForAdoptionIntegrationEventHandler> _logger;
        private readonly HubConnection _hubConnection;
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PetFlaggerForAdoptionIntegrationEventHandler(
            ILogger<PetFlaggerForAdoptionIntegrationEventHandler> logger,
            IServiceScopeFactory serviceScopeFactory,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _serviceScopeFactory = serviceScopeFactory;

            _hubConnection = new HubConnectionBuilder()
                .WithUrl(_configuration["serverurl"] + "/messageHub")
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.On<string>("ReceiveMessage", async (jsonBody) =>  await ReceiveMessage(jsonBody));
        }

        private async Task ReceiveMessage(string jsonBody)
        {
            try
            {
                var eventData = JsonConvert.DeserializeObject<PetFlaggedForAdoptionIntegrationEvent>(jsonBody);
                _logger.LogInformation("Message received from simulator for pet: {Name}", eventData?.Name);
                using var scope = _serviceScopeFactory.CreateScope();
                var repo = scope.ServiceProvider.GetRequiredService<IGenericRepository<Domain.Entities.RescuedAnimal>>();
                var dbContext = scope.ServiceProvider.GetRequiredService<RescueDbContext>();
                dbContext.RescueAnimalsMetadata.Add(eventData ??
                    throw new ArgumentException("Invalid event data."));
                var rescuedAnimal = new RescuedAnimal(eventData.Id);
                await repo.AddAsync(rescuedAnimal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message: {Message}", ex.Message);
            }
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