using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWindWebApi.DataAccessLayer;

namespace NorthWindWebApi.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
       builder.ToTable("products");
       builder.Property((p => p.Id)).HasColumnName("product_id");
    }
}