
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt => 
    opt.UseInMemoryDatabase("RescueDb"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Obtener todas las rescues
app.MapGet("/api/rescue", async (AppDbContext db) =>
    await db.Rescues.ToListAsync());

// Obtener mascota por Id
app.MapGet("/api/rescue/{id}", async (int id, AppDbContext db) =>
    await db.Rescues.FindAsync(id) is Rescue rescue ? Results.Ok(rescue) : Results.NotFound());

// Crear nueva mascota
app.MapPost("/api/rescue", async (Rescue rescue, AppDbContext db) =>
{
    db.Rescues.Add(rescue);
    await db.SaveChangesAsync();
    return Results.Created($"/api/rescues/{rescue.Id}", rescue);
});

app.Run();

