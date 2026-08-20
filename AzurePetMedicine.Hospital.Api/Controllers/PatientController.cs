using AzurePetMedicine.Hospital.Api.ApplicationServices;
using AzurePetMedicine.Hospital.Api.Command;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AzurePetMedicine.Hospital.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly ILogger<PatientController> _logger;
        private readonly Hospital.Api.ApplicationServices.PatientApplicationServices _rescueApplicationServices;

        public PatientController(ILogger<PatientController> logger, PatientApplicationServices rescueApplicationServices)
        {
            _logger = logger;
            _rescueApplicationServices = rescueApplicationServices;
        }

        [HttpPost("admit")]
        public async Task<IActionResult> CreateAdopter([FromBody] AdmitPatientCommand command)
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

        [HttpPost("discharge")]
        public async Task<IActionResult> DischargePatient([FromBody] DischargePatientCommand command)
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

        [HttpPut("bloodtype")]
        public async Task<IActionResult> SetAdopterBloodType([FromBody] SetBloodTypeCommand command)
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

        [HttpPut("weight")]
        public async Task<IActionResult> SetAdopterWeight([FromBody] SetWeightCommand command)
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