using AzurePetMedicine.Common.Events;

namespace AzurePetMedicine.Pet.Api.IntegrationEvents
{
    public record PetTransferredToHospitalIntegrationEvent(Guid Id, string Name, string Kind, int Age) : IIntegrationEvent { }
}