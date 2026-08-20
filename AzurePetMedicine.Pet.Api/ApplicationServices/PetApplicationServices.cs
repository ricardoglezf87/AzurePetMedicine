using AzurePetMedicine.Common.ApplicationServices;
using AzurePetMedicine.Common.Domains;
using AzurePetMedicine.Pet.Api.Commands;
using AzurePetMedicine.Pet.Api.IntegrationEvents;
using AzurePetMedicine.Pet.Domain.Events;
using AzurePetMedicine.Pet.Api.Commands;
using AzurePetMedicine.ServiceBus.Infrastructure;

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

        public async Task HandleCommandAsync(SetPetName command)
        {
            var pet = await _repository.GetByIdAsync(command.id)
                ?? throw new KeyNotFoundException($"Pet with ID {command.id} not found.");
            pet.Name = command.name;
            await _repository.UpdateAsync(pet);
        }

        public async Task HandleCommandAsync(SetPetKind command)
        {
            var pet = await _repository.GetByIdAsync(command.id)
                ?? throw new KeyNotFoundException($"Pet with ID {command.id} not found.");
            pet.Kind = command.kind;
            await _repository.UpdateAsync(pet);
        }

        public async Task HandleCommandAsync(SetPetAge command)
        {
            var pet = await _repository.GetByIdAsync(command.id)
                ?? throw new KeyNotFoundException($"Pet with ID {command.id} not found.");            
            pet.Age = command.age;
            await _repository.UpdateAsync(pet);
        }

        public async Task HandleCommandAsync(FlagPetForAdoptionCommand command)
        {
            var pet = await _repository.GetByIdAsync(command.id)                
                ?? throw new KeyNotFoundException($"Entity of type Pet with Id '{command.id}' was not found.");
            
            pet.FlagForAdoption();
        }
    }
}