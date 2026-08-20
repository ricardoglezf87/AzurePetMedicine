
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=Hospitals.db"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/api/hospital", async (AppDbContext db) =>
    await db.Hospitals.ToListAsync());

app.MapGet("/api/hospital/{id}", async (int id, AppDbContext db) =>
    await db.Hospitals.FindAsync(id) is Hospital hospital ? Results.Ok(hospital) : Results.NotFound());

app.MapPost("/api/hospital", async (Hospital hospital, AppDbContext db) =>
{
    db.Hospitals.Add(hospital);
    await db.SaveChangesAsync();
    return Results.Created($"/api/hospital/{hospital.Id}", hospital);
});

app.Run();

