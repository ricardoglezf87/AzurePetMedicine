using AzurePetMedicine.Pet.Api.ApplicationServices;
using AzurePetMedicine.Pet.Api.Commands;
using Microsoft.AspNetCore.Mvc;

namespace AzurePetMedicine.Pet.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetController : ControllerBase
    {
        private readonly ILogger<PetController> _logger;
        private readonly PetApplicationServices _petApplicationServices;

        public PetController(ILogger<PetController> logger, PetApplicationServices petApplicationServices)            
        {
            _logger = logger;
            _petApplicationServices = petApplicationServices;
        }

        [HttpPost]
        public async Task<ActionResult> CreatePet([FromBody] CreatePetCommand command)
        {
            try
            {                
                await _petApplicationServices.HandleCommandAsync(command);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPut("setname")]
        public async Task<ActionResult> SetPetName([FromBody] SetPetName command)
        {
            try
            {
                await _petApplicationServices.HandleCommandAsync(command);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPut("setkind")]
        public async Task<ActionResult> SetPetKind([FromBody] SetPetKind command)
        {
            try
            {
                await _petApplicationServices.HandleCommandAsync(command);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPut("setage")]
        public async Task<ActionResult> SetPetAge([FromBody] SetPetAge command)
        {
            try
            {
                await _petApplicationServices.HandleCommandAsync(command);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPost("flagforadoption")]
        public async Task<ActionResult> flagforadoption(FlagPetForAdoptionCommand command)
        {
            try
            {
                await _petApplicationServices.HandleCommandAsync(command);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogInformation(ex, ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPost("transferredtohospital")]
        public async Task<ActionResult> transferredtohospital(TransferredToHospitalCommand command)
        {
            try
            {
                await _petApplicationServices.HandleCommandAsync(command);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogInformation(ex, ex.Message);
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