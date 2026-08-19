using AzurePetMedicine.Common.Events;

namespace AzurePetMedicine.Pet.Domain.Events
{
    public static class DomainEvents
    {
        public static DomainEvent<PetFlaggedForAdoption> PetFlaggedForAdoption = new ();
    }
}