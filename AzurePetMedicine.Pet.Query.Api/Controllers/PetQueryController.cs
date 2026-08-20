using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace AzurePetMedicine.Pet.Query.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetQueryController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PetQueryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            string sql = @"
                SELECT *
                FROM Pets";
            using var connection = new SqliteConnection("Data Source=../Pets.db");            
            var orderDetail = (await connection.QueryAsync(sql)).ToList();            
            return Ok(orderDetail);
        }
    }
}
