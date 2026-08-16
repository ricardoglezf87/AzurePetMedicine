using Microsoft.EntityFrameworkCore;

public class Pet
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string kind { get; set; } = string.Empty;
    public int Age { get; set; }
}

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }
    public DbSet<Pet> Pets => Set<Pet>();
}