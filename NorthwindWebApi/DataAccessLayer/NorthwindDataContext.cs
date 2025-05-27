using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NorthWindWebApi.Configuration;
using NorthwindWebApi.Security;
using NorthwindWebApi.DataAccessLayer.Configuration;
using NorthwindWebApi.DataAccessLayer;


namespace NorthWindWebApi.DataAccessLayer;

public class NorthwindDataContext : IdentityDbContext<ApplicationUser , ApplicationRole, Guid>
{
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { set; get; }
    
    public NorthwindDataContext(DbContextOptions options) : base(options)
    {
              
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.HasSequence("product_sequence").StartsAt(100).IncrementsBy(1);
        modelBuilder.HasSequence("supplier_sequence").StartsAt(100).IncrementsBy(1);
        
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new SupplierConfiguration());
      
    }
}