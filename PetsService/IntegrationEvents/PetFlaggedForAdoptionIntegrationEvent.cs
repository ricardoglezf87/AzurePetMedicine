using PetsService.Common;

namespace PetsService.IntegrationEvents
{
    public record PetFlaggedForAdoptionIntegrationEvent(int Id, string Name, string Kind, int Age) : IIntegrationEvent { }
}