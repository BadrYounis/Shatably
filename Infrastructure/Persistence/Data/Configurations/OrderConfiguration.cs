using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.OrderId);

        builder.Property(o => o.OrderId)
            .ValueGeneratedOnAdd();

        builder.Property(o => o.OrderDate)
               .IsRequired();

        builder.Property(o => o.TotalPrice)
               .HasColumnType("decimal(18,2)");

        builder.Property(o => o.Status)
               .IsRequired()
               .HasMaxLength(50);

        builder.HasOne(o => o.Client)
               .WithMany(c => c.Orders)
               .HasForeignKey(o => o.ClientId);
    }
}

