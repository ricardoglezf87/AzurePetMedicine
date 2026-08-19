using AzurePetMedicine.Common.Api;
using AzurePetMedicine.Pet.Api.ApplicationServices;
using Microsoft.AspNetCore.Mvc;

namespace AzurePetMedicine.Pet.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetController : GenericController<Domain.Entities.Pet>
    {
        private readonly ILogger<PetController> _logger;
        private readonly PetApplicationServices petApplicationServices;

        public PetController(ILogger<PetController> logger, PetApplicationServices petApplicationServices)
            : base(petApplicationServices, logger)
        {
            _logger = logger;
            this.petApplicationServices = petApplicationServices;
        }


        [HttpPost("flagforadoption")]
        public async Task<ActionResult> flagforadoption(Guid Id)
        {
            try
            {
                await petApplicationServices.flagforadoption(Id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                Logger.LogInformation(ex, ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}