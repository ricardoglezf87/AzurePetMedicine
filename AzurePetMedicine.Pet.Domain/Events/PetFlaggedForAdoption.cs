using AzurePetMedicine.Common;

namespace AzurePetMedicine.Pet.Domain.Events
{
    public record PetFlaggedForAdoption(Guid Id, string Name, string Kind, int Age) : IDomainEvent {}
}   