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
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.category)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.stockAmount)
            .IsRequired();

        builder.Property(p => p.isPackage)
            .IsRequired();

        builder.HasMany(p => p.OrderItems)
            .WithOne(oi => oi.Product)
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}