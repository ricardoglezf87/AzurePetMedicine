using AzurePetMedicine.Common.ApplicationServices;
using AzurePetMedicine.Common.Domains;
using AzurePetMedicine.Pet.Domain.Events;
using AzurePetMedicine.Pet.Api.IntegrationEvents;
using AzurePetMedicine.ServiceBus.Infrastructure;
using AzurePetMedicine.Pet.Api.Commands;

namespace AzurePetMedicine.Pet.Api.ApplicationServices
{   
    public class PetApplicationServices 
    {         
        private readonly IGenericRepository<Domain.Entities.Pet> _repository;

        public PetApplicationServices(
            IGenericRepository<Domain.Entities.Pet> repository,            
            IEventPublisher eventPublisher)             
        {
            _repository = repository;

            DomainEvents.PetFlaggedForAdoption.Register(async c =>
            {
                var integrationEventHandler = new PetFlaggedForAdoptionIntegrationEvent(c.Id, c.Name, c.Kind, c.Age);
                await eventPublisher.PublishAsync(integrationEventHandler, "adoption-topic");
            });
        }

        public async Task HandleCommandAsync(CreatePetCommand command)
        {
            var pet = new Domain.Entities.Pet()
            {                
                Name = command.Name,
                Kind = command.Kind,
                Age = command.Age
            };

            await _repository.AddAsync(pet);
        }

        public async Task flagforadoption(Guid Id)
        {
            var pet = await _repository.GetByIdAsync(Id)                
                ?? throw new KeyNotFoundException($"Entity of type Pet with Id '{Id}' was not found.");
            
            pet.FlagForAdoption();
        }
    }
}