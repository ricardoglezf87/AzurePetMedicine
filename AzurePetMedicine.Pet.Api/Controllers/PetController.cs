using AzurePetMedicine.Pet.Api.ApplicationServices;
using AzurePetMedicine.Pet.Api.Command;
using AzurePetMedicine.Pet.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace AzurePetMedicine.Pet.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetController : ControllerBase
    {
        private readonly ILogger<PetController> _logger;
        private readonly PetApplicationServices petApplicationServices;

        public PetController(ILogger<PetController> logger, PetApplicationServices petApplicationServices)
        {
            _logger = logger;
            this.petApplicationServices = petApplicationServices;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePetCommand command)
        {
            try
            {
                await petApplicationServices.HandleCommandAsync(command);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing CreatePetCommand.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdatePetCommand command)
        {
            try
            {
                await petApplicationServices.HandleCommandAsync(command);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing UpdatePetCommand.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                // Llamamos a la consulta corregida que devuelve List<Pet>
                var pets = await petApplicationServices.HandleQueryAsync(new GetAllPetsQuery());
                return Ok(pets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all pets.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var query = new GetPetByIdQuery (id);
                var pet = await petApplicationServices.HandleQueryAsync(query);
                return Ok(pet);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Pet with id {id} not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting pet {id}");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(Guid id)
        {
            try
            {
                var command = new DeletePetCommand(id);
                await petApplicationServices.HandleCommandAsync(command);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Pet with id {id} not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting pet {id}");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

    }
}