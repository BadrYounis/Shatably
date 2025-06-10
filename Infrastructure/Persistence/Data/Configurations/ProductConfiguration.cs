using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.productId);

        builder.Property(p => p.productId)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.name)
            .HasMaxLength(100);

        builder.Property(p => p.category)
            .HasMaxLength(50);

        builder.Property(p => p.price)
            .HasColumnType("decimal(18,2)");
    }
}