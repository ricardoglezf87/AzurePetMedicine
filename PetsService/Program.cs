
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt => 
    opt.UseInMemoryDatabase("PetDb"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Obtener todas las pets
app.MapGet("/api/pet", async (AppDbContext db) =>
    await db.Pets.ToListAsync());

// Obtener mascota por Id
app.MapGet("/api/pet/{id}", async (int id, AppDbContext db) =>
    await db.Pets.FindAsync(id) is Pet pet ? Results.Ok(pet) : Results.NotFound());

// Crear nueva mascota
app.MapPost("/api/pet", async (Pet pet, AppDbContext db) =>
{
    db.Pets.Add(pet);
    await db.SaveChangesAsync();
    return Results.Created($"/api/pets/{pet.Id}", pet);
});

app.Run();

