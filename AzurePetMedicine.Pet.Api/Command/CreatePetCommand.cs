namespace AzurePetMedicine.Pet.Api.Command
{
    public record CreatePetCommand(Guid Id, string Name, string Kind, int Age);

}
