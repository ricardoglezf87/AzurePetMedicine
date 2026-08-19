namespace AzurePetMedicine.Pet.Api.Command;

public record CreatePetCommand(Guid Id, string Name, string Kind, int Age);
public record DeletePetCommand(Guid Id);
public record GetAllPetsQuery();
public record GetPetByIdQuery(Guid Id);
public record UpdatePetCommand(Guid Id, string Name, string Kind, int Age);
public record FlagPetForAdoptionCommand(Guid Id);

