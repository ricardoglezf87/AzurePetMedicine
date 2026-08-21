namespace AzurePetMedicine.Hospital.Api.IntegrationEvents;

public record PetTransferredToHospitalIntegrationEvent(
    Guid id,
    string name,
    string kind,
    int age);
