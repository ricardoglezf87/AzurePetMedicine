namespace AzurePetMedicine.Pet.Api.Commands;

public record CreatePetCommand(    
    string Name,
    string Kind,
    int Age
);

