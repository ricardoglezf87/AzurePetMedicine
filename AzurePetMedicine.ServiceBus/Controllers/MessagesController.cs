using Azure.Messaging.ServiceBus;
using AzurePetMedicine.ServiceBus.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AzurePetMedicine.ServiceBus.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {       
        [HttpPost("publish/{topicName}")]
        public async Task<IActionResult> Publish(
        string topicName,
        [FromBody] MessageDto dto,
        [FromServices] ServiceBusClient sbClient)
        {
            var sender = sbClient.CreateSender(topicName);

            var message = new ServiceBusMessage(dto.Body)
            {
                MessageId = Guid.NewGuid().ToString(),
                Subject = dto.Subject,
                ContentType = "application/json"
            };

            await sender.SendMessageAsync(message);

            return Ok(new { Status = "Encolado en Spotflow", MessageId = message.MessageId });
        }
    }

    
}
