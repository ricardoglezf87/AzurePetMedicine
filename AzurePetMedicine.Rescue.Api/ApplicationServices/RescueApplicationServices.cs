using AzurePetMedicine.Common.ApplicationServices;
using AzurePetMedicine.Common.Domains;
using AzurePetMedicine.ServiceBus.Infrastructure;
using System.Transactions;

namespace AzurePetMedicine.Rescue.Api.ApplicationServices
{   
    public class RescueApplicationServices : GenericCrudService<Domain.Entities.Rescue>
    {
        public RescueApplicationServices(
            IGenericRepository<Domain.Entities.Rescue> repository)
            : base(repository)
        {            
        }
        

    }
}