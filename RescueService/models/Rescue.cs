using Microsoft.EntityFrameworkCore;

public class Rescue
{
    public int Id { get; set; }
    public DateTime Date { get; set; } = DateTime.MinValue;
    public string Description { get; set; } = string.Empty;    
}

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }
    public DbSet<Rescue> Rescues => Set<Rescue>();
}