using Microsoft.AspNetCore.SignalR;

namespace AzurePetMedicine.ServiceBus.Server.Hubs
{
    public class MessageHub : Hub
    {
        public async Task Subscribe(string topicName, string subscriptionName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"{topicName}/{subscriptionName}");
        }
    }
}
