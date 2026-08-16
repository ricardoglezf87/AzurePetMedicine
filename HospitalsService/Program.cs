
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt => 
    opt.UseInMemoryDatabase("HospitalDb"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Obtener todas las hospitals
app.MapGet("/api/hospital", async (AppDbContext db) =>
    await db.Hospitals.ToListAsync());

// Obtener mascota por Id
app.MapGet("/api/hospital/{id}", async (int id, AppDbContext db) =>
    await db.Hospitals.FindAsync(id) is Hospital hospital ? Results.Ok(hospital) : Results.NotFound());

// Crear nueva mascota
app.MapPost("/api/hospital", async (Hospital hospital, AppDbContext db) =>
{
    db.Hospitals.Add(hospital);
    await db.SaveChangesAsync();
    return Results.Created($"/api/hospital/{hospital.Id}", hospital);
});

app.Run();

