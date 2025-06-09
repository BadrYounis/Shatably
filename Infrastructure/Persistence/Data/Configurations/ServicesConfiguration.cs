using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data
{
    public class ServicesConfiguration : IEntityTypeConfiguration<ServicesWork>
    {
        public void Configure(EntityTypeBuilder<ServicesWork> builder)
        {
            // Primary Key
            builder.HasKey(s => s.ServiceId);

            // Properties
            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Details)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(s => s.Description)
                .HasMaxLength(500);

            builder.Property(s => s.ImageUrl)
                .IsRequired();

            builder.Property(s => s.Price)
                .HasColumnType("decimal(18,2)");

            // Relationships

            builder.HasMany(s => s.RatingReviews)
                .WithOne()
                .HasForeignKey("ServiceId") // assuming there's a foreign key in RatingReview
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(s => s.ServiceBookings)
                .WithOne()
                .HasForeignKey("ServiceId") // assuming there's a foreign key in ServiceBooking
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}