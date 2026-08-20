using AzurePetMedicine.Common.Domains;
using Microsoft.EntityFrameworkCore;

namespace AzurePetMedicine.Hospital.Api.Infrastructure
{
    public class PatientRepository : IGenericRepository<Domain.Entities.Patient>
    {
        private readonly HospitalDbContext _context;

        public PatientRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Patient?> GetByIdAsync(Guid id)
        {
            return await _context.Patients.FindAsync(id);            
        }

        public async Task AddAsync(Domain.Entities.Patient entity)
        {
            await _context.Patients.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Domain.Entities.Patient hospital)
        {
            _context.Patients.Update(hospital);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var hospital = await _context.Patients.FindAsync(id);
            if (hospital != null)
            {
                _context.Patients.Remove(hospital);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Domain.Entities.Patient>> GetAllAsync()
        {
            return await _context.Patients.ToListAsync<Domain.Entities.Patient>();
        }
       
    }
}
