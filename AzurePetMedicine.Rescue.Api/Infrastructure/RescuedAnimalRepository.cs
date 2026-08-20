using AzurePetMedicine.Common.Domains;
using Microsoft.EntityFrameworkCore;

namespace AzurePetMedicine.Rescue.Api.Infrastructure
{
    public class RescuedAnimalRepository : IGenericRepository<Domain.Entities.RescuedAnimal>
    {
        private readonly RescueDbContext _context;

        public RescuedAnimalRepository(RescueDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.RescuedAnimal?> GetByIdAsync(Guid id)
        {
            return await _context.RescuedAnimals.FindAsync(id);            
        }

        public async Task AddAsync(Domain.Entities.RescuedAnimal entity)
        {
            await _context.RescuedAnimals.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Domain.Entities.RescuedAnimal rescuedAnimal)
        {
            _context.RescuedAnimals.Update(rescuedAnimal);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var rescuedAnimal = await _context.RescuedAnimals.FindAsync(id);
            if (rescuedAnimal != null)
            {
                _context.RescuedAnimals.Remove(rescuedAnimal);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Domain.Entities.RescuedAnimal>> GetAllAsync()
        {
            return await _context.RescuedAnimals.ToListAsync<Domain.Entities.RescuedAnimal>();
        }
       
    }
}
