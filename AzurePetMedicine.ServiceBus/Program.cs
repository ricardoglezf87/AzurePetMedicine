using Azure.Messaging.ServiceBus;
using AzurePetMedicine.ServiceBus.Server;
using AzurePetMedicine.ServiceBus.Server.Hubs;
using Microsoft.Azure.Amqp.Framing;
using Spotflow.InMemory.Azure.ServiceBus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddHealthChecks();
builder.Services.AddSwaggerGen();

var provider = new InMemoryServiceBusProvider();
var ns = provider.AddNamespace("AzurePetMedicine");

var topicsWithSubs = builder.Configuration
    .GetSection("ServiceBus:TopicsWithSubscriptions")
    .Get<Dictionary<string, List<string>>>() ?? new Dictionary<string, List<string>>();

foreach (var (topicName, subscriptions) in topicsWithSubs)
{
    var topicState = ns.AddTopic(topicName);
    if (subscriptions != null)
    {
        foreach (var subName in subscriptions)
        {
            topicState.AddSubscription(subName);
        }
    }
}

string cs = "Endpoint=sb://AzurePetMedicine.servicebus.in-memory.example.com/;SharedAccessKeyName=Root;SharedAccessKey=fake";
var client = new InMemoryServiceBusClient(cs, provider);

builder.Services.AddSingleton(provider);
builder.Services.AddSingleton<ServiceBusClient>(client);
builder.Services.AddHostedService<SpotflowQueueListener>();

var app = builder.Build();

app.UseRouting();
app.MapControllers();
app.MapHub<MessageHub>("/messageHub");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.Run();