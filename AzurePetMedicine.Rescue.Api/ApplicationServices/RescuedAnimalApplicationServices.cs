using AzurePetMedicine.Common.ApplicationServices;
using AzurePetMedicine.Common.Domains;
using AzurePetMedicine.ServiceBus.Infrastructure;
using System.Transactions;

namespace AzurePetMedicine.Rescue.Api.ApplicationServices
{   
    public class RescuedAnimalApplicationServices : GenericCrudService<Domain.Entities.RescuedAnimal>
    {
        public RescuedAnimalApplicationServices(
            IGenericRepository<Domain.Entities.RescuedAnimal> repository)
            : base(repository)
        {            
        }
        

    }
}