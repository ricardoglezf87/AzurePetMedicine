using PetsService.Common;

namespace PetsService.Events
{
    public record PetFlaggedForAdoption(int Id, string Name, string Kind, int Age):IDomainEvent{}
}