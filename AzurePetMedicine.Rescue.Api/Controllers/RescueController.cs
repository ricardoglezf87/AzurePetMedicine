using AzurePetMedicine.Common.Api;
using AzurePetMedicine.Rescue.Api.ApplicationServices;
using Microsoft.AspNetCore.Mvc;

namespace AzurePetMedicine.Rescue.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RescueController : GenericController<Domain.Entities.Rescue>
    {
        private readonly ILogger<RescueController> _logger;
        private readonly RescueApplicationServices _rescueApplicationServices;

        public RescueController(ILogger<RescueController> logger, RescueApplicationServices rescueApplicationServices)
            : base(rescueApplicationServices, logger)
        {
            _logger = logger;
            _rescueApplicationServices = rescueApplicationServices;
        }
    }
}