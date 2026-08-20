using AzurePetMedicine.Common.Api;
using AzurePetMedicine.Rescue.Api.ApplicationServices;
using Microsoft.AspNetCore.Mvc;

namespace AzurePetMedicine.Rescue.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RescuedAnimalController : GenericController<Domain.Entities.RescuedAnimal>
    {
        private readonly ILogger<RescuedAnimalController> _logger;
        private readonly RescuedAnimalApplicationServices _rescuedAnimalApplicationServices;

        public RescuedAnimalController(ILogger<RescuedAnimalController> logger, RescuedAnimalApplicationServices rescuedAnimalApplicationServices)
            : base(rescuedAnimalApplicationServices, logger)
        {
            _logger = logger;
            _rescuedAnimalApplicationServices = rescuedAnimalApplicationServices;
        }
    }
}