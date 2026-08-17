namespace AzurePetMedicine.Pet.Api.Command
{
    public record UpdatePetCommand(Guid Id, string Name, string Kind, int Age);
}
