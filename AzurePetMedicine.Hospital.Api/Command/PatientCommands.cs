namespace AzurePetMedicine.Hospital.Api.Command;

public record AdmitPatientCommand(Guid id);
public record SetBloodTypeCommand(Guid id, string bloodType);
public record SetWeightCommand(Guid id, decimal weight);
public record DischargePatientCommand(Guid id);