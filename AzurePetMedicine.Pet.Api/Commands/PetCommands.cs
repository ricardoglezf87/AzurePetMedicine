namespace AzurePetMedicine.Pet.Api.Commands;

public record CreatePetCommand(    
    string Name,
    string Kind,
    int Age
);

public record SetPetName(Guid id, string name);
public record SetPetKind(Guid id, string kind);
public record SetPetAge(Guid id, int age);
public record TransferredToHospitalCommand(Guid id);
public record FlagPetForAdoptionCommand(Guid id);
