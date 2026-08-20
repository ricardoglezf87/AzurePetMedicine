using AzurePetMedicine.Common.Domains;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace AzurePetMedicine.Common.ApplicationServices
{
    public class GenericCrudService<TEntity>
        where TEntity : class, IEntity, IMappableEntity, new()
    {
        protected readonly IGenericRepository<TEntity> Repository;

        public GenericCrudService(IGenericRepository<TEntity> repository)
        {
            Repository = repository;
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await Repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Entity of type {typeof(TEntity).Name} with Id '{id}' was not found.");
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await Repository.GetAllAsync();
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id) ?? throw new KeyNotFoundException($"Entity of type {typeof(TEntity).Name} with Id '{id}' was not found."); 
            await Repository.DeleteAsync(id);
        }

        public virtual async Task<TEntity> CreateAsync(TEntity dto)
        {
            var entity = new TEntity();
            entity.MapFromEntity(dto);

            await Repository.AddAsync(entity);
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity dto)
        {
            var entity = await Repository.GetByIdAsync(dto.Id)
                ?? throw new KeyNotFoundException($"Entity with Id '{dto.Id}' not found.");

            entity.MapFromEntity(dto);

            await Repository.UpdateAsync(entity);
            return entity;
        }
    }

}
