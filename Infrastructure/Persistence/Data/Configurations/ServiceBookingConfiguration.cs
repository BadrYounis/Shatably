using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;
public class ServiceBookingConfiguration: IEntityTypeConfiguration<ServiceBooking>
{
    public void Configure(EntityTypeBuilder<ServiceBooking> builder)
    {
        // Primary Key
        builder.HasKey(sb => sb.bookingId);

        // Properties
        builder.Property(sb => sb.bookingId)
               .IsRequired();

        builder.Property(sb => sb.bookingDate)
               .IsRequired();

        builder.Property(sb => sb.Status)
               .IsRequired()
               .HasMaxLength(50); 

        builder.Property(sb => sb.TimeSlot)
               .IsRequired()
               .HasMaxLength(50); 

        // Relationships
        builder.HasOne(sb => sb.Client)
               .WithMany(c=>c.ServiceBookings) 
               .HasForeignKey(sb => sb.ClientId)
               .OnDelete(DeleteBehavior.Cascade); 

        builder.HasOne(sb => sb.Worker)
               .WithMany(w=>w.ServiceBookings) 
               .HasForeignKey(sb => sb.WorkerId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(sb => sb.Portfolio)
               .WithMany(w => w.ServiceBookings)
               .HasForeignKey(sb => sb.PortfolioId)
               .OnDelete(DeleteBehavior.Cascade);

        // Table name 
        builder.ToTable("ServiceBookings");
    }
}
