using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oi => oi.orderItemId);

        builder.Property(oi => oi.orderItemId)
            .ValueGeneratedOnAdd();

        builder.Property(oi => oi.quantity)
               .IsRequired();

        builder.Property(oi => oi.unitPrice)
               .HasColumnType("decimal(18,2)");

        builder.HasOne(oi => oi.Order)
               .WithMany(o => o.OrderItems)
               .HasForeignKey(oi => oi.OrderId);

        builder.HasOne(oi => oi.Product)
               .WithMany()
               .HasForeignKey(oi => oi.ProductId);
    }
}
