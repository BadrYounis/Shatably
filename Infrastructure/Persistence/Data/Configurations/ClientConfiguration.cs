using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;
public class ClientProfileConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(c => c.ClientId);

        builder.Property(c => c.ClientId)
            .ValueGeneratedOnAdd();

        builder.Property(c => c.fullName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(c => c.email)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(c => c.phoneNumber)
               .HasMaxLength(20);

        builder.OwnsOne(c => c.Address, address =>
        {
            address.Property(a => a.city)
                  .HasColumnName("City")
                  .HasMaxLength(50);

            address.Property(a => a.region)
                   .HasColumnName("Region")
                   .HasMaxLength(50);
         
            address.Property(a => a.street)
                   .HasColumnName("Street")
                   .HasMaxLength(100);
        });

        //builder.Property(c => c.UserId)
        //       .IsRequired();

        builder.HasMany(c => c.Orders)
               .WithOne(o=>o.Client)
               .HasForeignKey(o => o.ClientId);

        builder.HasMany(c => c.RatingReviews)
               .WithOne(r => r.Client)
               .HasForeignKey(r => r.ClientId);

        // DesignRequest
        // builder.HasMany(c => c.DesignRequests)
        //        .WithOne(d => d.Client)
        //        .HasForeignKey(d => d.ClientId);
    }
}
