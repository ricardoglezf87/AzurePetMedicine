using AzurePetMedicine.Common.Events;

namespace AzurePetMedicine.Pet.Domain.IntegrationEvents
{
    public record PetFlaggedForAdoptionIntegrationEvent(Guid Id, string Name, string Kind, int Age) : IIntegrationEvent { }
}