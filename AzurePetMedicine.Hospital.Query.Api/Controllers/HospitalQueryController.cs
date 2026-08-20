using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace AzurePetMedicine.Rescue.Query.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HospitalQueryController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public HospitalQueryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            string sql = @"
                SELECT *
                FROM Patients";
            using var connection = new SqliteConnection("Data Source=../Hospitals.db");            
            var orderDetail = (await connection.QueryAsync(sql)).ToList();            
            return Ok(orderDetail);
        }
    }
}
