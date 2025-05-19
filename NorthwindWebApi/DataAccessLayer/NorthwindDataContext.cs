using Microsoft.EntityFrameworkCore;
using NorthWindWebApi.Configuration;


namespace NorthWindWebApi.DataAccessLayer;

public class NorthwindDataContext : DbContext
{
    
    public DbSet<Product> Products { get; set; }
    
    public NorthwindDataContext(DbContextOptions options) : base(options)
    {
              
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
    }
}