using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWindWebApi.DataAccessLayer;

namespace NorthwindWebApi.DataAccessLayer.Configuration
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("suppliers");
            builder.HasKey(s => s.SupplierId);
            builder.Property(s => s.SupplierId).HasColumnName("supplier_id").HasDefaultValueSql("nextval('\"product_sequence\"')");
            builder.Property(s => s.CompanyName).HasColumnName("company_name");
            builder.Property(s => s.ContactName).HasColumnName("contact_name");
            builder.Property(s => s.SupplierId).HasColumnName("supplier_id");
            builder.Property(s => s.ContactTitle).HasColumnName("contact_title");
            builder.Property(s => s.Address).HasColumnName("address");
            builder.Property(s => s.City).HasColumnName("city");
            builder.Property(s => s.Region).HasColumnName("region");
            builder.Property(s => s.PostalCode).HasColumnName("postal_code");
            builder.Property(s => s.Country).HasColumnName("country");
            builder.Property(s => s.Phone).HasColumnName("phone");
            builder.Property(s => s.Fax).HasColumnName("fax");
            builder.Property(s => s.HomePage).HasColumnName("homepage");
            
            //builder.HasMany(s => s.Products).WithOne(p => p.Supplier).HasForeignKey(p => p.SupplierId).HasPrincipalKey(s => s.SupplierId);
        }
    }
}
