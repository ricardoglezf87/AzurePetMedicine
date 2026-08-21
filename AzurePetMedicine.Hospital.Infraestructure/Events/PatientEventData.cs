namespace AzurePetMedicine.Hospital.Infraestructure.Events;

public record PatientEventData(
    Guid Id,
    string aggregateId,
    string EventName,
    string data,
    string AssemblyQualifiedName
);
