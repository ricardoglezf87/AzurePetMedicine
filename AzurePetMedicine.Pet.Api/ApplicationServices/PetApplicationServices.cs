using AzurePetMedicine.Common.ApplicationServices;
using AzurePetMedicine.Common.Domains;
using AzurePetMedicine.Pet.Domain.Events;
using AzurePetMedicine.Pet.Api.IntegrationEvents;
using AzurePetMedicine.ServiceBus.Infrastructure;

namespace AzurePetMedicine.Pet.Api.ApplicationServices
{   
    public class PetApplicationServices : GenericCrudService<Domain.Entities.Pet>
    {               
        public PetApplicationServices(
            IGenericRepository<Domain.Entities.Pet> repository,            
            IEventPublisher eventPublisher) 
            : base(repository)
        {
            DomainEvents.PetFlaggedForAdoption.Register(async c =>
            {
                var integrationEventHandler = new PetFlaggedForAdoptionIntegrationEvent(c.Id, c.Name, c.Kind, c.Age);
                await eventPublisher.PublishAsync(integrationEventHandler, "adoption-topic");
            });
        }

        public async Task flagforadoption(Guid Id)
        {
            var pet = await Repository.GetByIdAsync(Id)                
                ?? throw new KeyNotFoundException($"Entity of type Pet with Id '{Id}' was not found.");
            
            pet.FlagForAdoption();
        }
    }
}