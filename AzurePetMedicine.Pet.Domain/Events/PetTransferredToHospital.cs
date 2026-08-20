using AzurePetMedicine.Common.Events;

namespace AzurePetMedicine.Pet.Domain.Events
{
    public record PetTransferredToHospital(Guid Id, string Name, string Kind, int Age) : IDomainEvent {}
}   