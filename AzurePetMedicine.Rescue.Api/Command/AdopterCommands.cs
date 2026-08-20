namespace AzurePetMedicine.Rescue.Api.Command;

public record CreateAdopterCommand(Guid id, string name, string phoneNumber);
public record SetAdopterPhoneNumberCommand(Guid id, string phoneNumber);