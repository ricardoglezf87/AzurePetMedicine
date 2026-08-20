namespace AzurePetMedicine.Common.Domains
{
    public interface IMappableEntity
    {
        void MapFromEntity(object entity);
    }
}
