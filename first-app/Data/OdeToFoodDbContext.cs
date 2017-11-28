using Microsoft.EntityFrameworkCore;

public class OdeToFoodDbContext : DbContext
{
    public OdeToFoodDbContext(DbContextOptions options)
        : base (options)
    {
    }
    
    public DbSet<Restaurant> Restaurants { get; set; }
}