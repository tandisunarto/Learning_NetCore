using Microsoft.EntityFrameworkCore;
using System.Configuration;

public class FirstContext : DbContext
{

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlServer("Server=ICF2008640-VM2;Database=TEST_EF;User Id=sa;Password=Password!1");
    }
    
    public DbSet<Employee> Employees { get; set; }
}