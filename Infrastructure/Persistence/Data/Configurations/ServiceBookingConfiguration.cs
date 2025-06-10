using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;
public class ServiceBookingConfiguration: IEntityTypeConfiguration<ServiceBooking>
{
    public void Configure(EntityTypeBuilder<ServiceBooking> builder)
    {
        // Table name
        builder.ToTable("ServiceBookings");

        builder.HasKey(sb => sb.BookingId);

        builder.Property(sb => sb.BookingId)
               .IsRequired();

        builder.Property(sb => sb.Status)
               .HasMaxLength(50);

        builder.Property(sb => sb.TimeSlot)
               .HasMaxLength(50);

        builder.Property(sb => sb.TotalPrice)
               .HasColumnType("decimal(18,3)");

        builder.Property(sb => sb.PaymentStatus)
               .HasConversion<string>()
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(sb => sb.ClientId).IsRequired(false);

        builder.Property(sb => sb.WorkerId).IsRequired(false); // مهم

        builder.Property(sb => sb.PortfolioId).IsRequired(false); // مهم

        builder.Property(sb => sb.ServiceId).IsRequired(false);

        builder.Property(sb => sb.PaymentIntentId)
               .HasMaxLength(100);

        builder.Property(sb => sb.ClientSecret)
               .HasMaxLength(255);

        builder.Property(sb => sb.Address)
               .HasMaxLength(250);

        builder.Property(sb => sb.Description)
               .HasMaxLength(500);

        builder.Property(sb => sb.Latitude)
               .HasColumnType("float"); 

        builder.Property(sb => sb.Longitude)
               .HasColumnType("float");

        // Relationships
        builder.HasOne(sb => sb.Client)
       .WithMany(c => c.ServiceBookings)
       .HasForeignKey(sb => sb.ClientId)
       .OnDelete(DeleteBehavior.NoAction); // ClientId is optional

        builder.HasOne(sb => sb.Worker)
               .WithMany(w => w.ServiceBookings)
               .HasForeignKey(sb => sb.WorkerId)
               .OnDelete(DeleteBehavior.NoAction); // WorkerId is optional

        builder.HasOne(sb => sb.Portfolio)
               .WithMany(p => p.ServiceBookings)
               .HasForeignKey(sb => sb.PortfolioId)
               .OnDelete(DeleteBehavior.NoAction); // PortfolioId is optional

        builder.HasOne(sb => sb.Services)
               .WithMany(s => s.ServiceBookings)
               .HasForeignKey(sb => sb.ServiceId)
               .OnDelete(DeleteBehavior.NoAction); // ServiceId is optional
    }
}