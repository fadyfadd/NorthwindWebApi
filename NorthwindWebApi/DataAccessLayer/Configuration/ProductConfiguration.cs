using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWindWebApi.DataAccessLayer;

namespace NorthWindWebApi.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");
        builder.HasKey(p => p.ProductId);
        builder.Property((p => p.ProductId)).HasColumnName("product_id");
        builder.Property(p => p.ProductName).HasColumnName("product_name");
        builder.Property(p => p.SupplierId).HasColumnName("supplier_id");
        builder.Property(p => p.CategoryId).HasColumnName("category_id");
        builder.Property(p => p.QuantityPerUnit).HasColumnName("quantity_per_unit");
        builder.Property(p => p.UnitPrice).HasColumnName("unit_price");
        builder.Property(p => p.UnitsInStock).HasColumnName("units_in_stock");
        builder.Property(p => p.UnitsOnOrder).HasColumnName("units_on_order");
        builder.Property(p => p.ReorderLevel).HasColumnName("reorder_level");
        builder.Property(p => p.QuantityPerUnit).HasColumnName("quantity_per_unit");
        builder.Property(p => p.Discontinued).HasColumnName("discontinued");
    }
}