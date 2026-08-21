using AzurePetMedicine.Common.Domains;
using AzurePetMedicine.Hospital.Api.Infrastructure;
using AzurePetMedicine.Hospital.Domain.Entities;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

namespace AzurePetMedicine.Hospital.Api.IntegrationEvents
{
    public class PetTransferredToHospitalIntegrationEventHandle : BackgroundService
    {
        private readonly ILogger<PetTransferredToHospitalIntegrationEventHandle> _logger;
        private readonly HubConnection _hubConnection;
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PetTransferredToHospitalIntegrationEventHandle(
            ILogger<PetTransferredToHospitalIntegrationEventHandle> logger,
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

            _hubConnection.On<string>("ReceiveMessage", async (jsonBody) => await ReceiveMessage(jsonBody));
        }

        private async Task ReceiveMessage(string jsonBody)
        {
            try
            {
                var eventData = JsonConvert.DeserializeObject<PetTransferredToHospitalIntegrationEvent>(jsonBody);
                _logger.LogInformation("Message received from simulator for pet: {name}", eventData?.name);
                using var scope = _serviceScopeFactory.CreateScope();
                var repo = scope.ServiceProvider.GetRequiredService<IGenericRepository<Domain.Entities.Patient>>();
                var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
                if(dbContext.PatientMetadata.Find(eventData?.id) == null)
                {
                    dbContext.PatientMetadata.Add(eventData ??
                    throw new ArgumentException("Invalid event data."));
                }               
                var hospitalizedAnimal = new Patient(eventData.id);
                await repo.AddAsync(hospitalizedAnimal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message: {Message}", ex.Message);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _hubConnection.StartAsync(stoppingToken);
            await _hubConnection.InvokeAsync("Subscribe", "hospital-topic", "hospital-transfer", stoppingToken);
            _logger.LogInformation("Successfully subscribed to hospital event channel.");
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _hubConnection.StopAsync(cancellationToken);
            await _hubConnection.DisposeAsync();
            _logger.LogInformation("Disconnected from PetTransferredToHospitalIntegrationEvent");
            await base.StopAsync(cancellationToken);
        }
    }
}
