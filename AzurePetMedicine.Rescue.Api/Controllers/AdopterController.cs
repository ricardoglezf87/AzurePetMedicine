using AzurePetMedicine.Rescue.Api.ApplicationServices;
using AzurePetMedicine.Rescue.Api.Command;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AzurePetMedicine.Rescue.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdopterController : ControllerBase
    {
        private readonly ILogger<AdopterController> _logger;
        private readonly AdopterApplicationServices _rescueApplicationServices;

        public AdopterController(ILogger<AdopterController> logger, AdopterApplicationServices rescueApplicationServices)
        {
            _logger = logger;
            _rescueApplicationServices = rescueApplicationServices;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdopter([FromBody] CreateAdopterCommand command)
        {
            try
            {
                await _rescueApplicationServices.HandleCommandAsync(command);
                return new OkResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPut("phonenumber")]
        public async Task<IActionResult> SetAdopterPhoneNumber([FromBody] SetAdopterPhoneNumberCommand command)
        {
            try
            {
                await _rescueApplicationServices.HandleCommandAsync(command);
                return new OkResult();
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