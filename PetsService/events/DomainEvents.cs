using PetsService.Common;

namespace PetsService.Events
{
    public static class DomainEvents
    {
        public static DomainEvent<PetFlaggedForAdoption> PetFlaggedForAdoption = new ();
    }
}