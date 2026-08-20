using AzurePetMedicine.Common.Events;

namespace AzurePetMedicine.Rescue.Api.IntegrationEvents
{
    public record PetFlaggedForAdoptionIntegrationEvent(Guid Id, string Name, string Kind, int Age);
}