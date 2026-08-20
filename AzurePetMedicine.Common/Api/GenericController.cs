using AzurePetMedicine.Common.ApplicationServices;
using AzurePetMedicine.Common.Domains;
using Microsoft.AspNetCore.Mvc;

namespace AzurePetMedicine.Common.Api
{
    [ApiController]
    [Route("[controller]")]
    public abstract class GenericController<TEntity> : ControllerBase
        where TEntity : class, IEntity, IMappableEntity, new()
    {
        protected readonly GenericCrudService<TEntity> Service;
        protected readonly ILogger Logger;

        protected GenericController(GenericCrudService<TEntity> service, ILogger logger)
        {
            Service = service;
            Logger = logger;
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] TEntity Entity)
        {
            try
            {
                await Service.CreateAsync(Entity);
                return Ok();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TEntity Entity)
        {
            try
            {
                await Service.UpdateAsync(Entity);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                Logger.LogInformation(ex, ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll()
        {
            try
            {
                var list = await Service.GetAllAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var entity = await Service.GetByIdAsync(id);
                return Ok(entity);
            }
            catch (KeyNotFoundException ex)
            {
                Logger.LogInformation(ex, ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await Service.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                Logger.LogInformation(ex, ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}
