using PetsService.Events;
using PetsService.IntegrationEvents;

namespace PetsService.ApplicationServices
{   
    public class PetApplicationServices
    {

        public PetApplicationServices()
        {
            DomainEvents.PetFlaggedForAdoption.Register( c=>
            {
                var integrationEventHandler = new PetFlaggedForAdoptionIntegrationEvent(c.Id, c.Name, c.Kind, c.Age);
            });
        }
    }
}