using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NorthWindWebApi.Configuration;
using NorthwindWebApi.Security;


namespace NorthWindWebApi.DataAccessLayer;

public class NorthwindDataContext : IdentityDbContext<ApplicationUser , ApplicationRole, Guid>
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