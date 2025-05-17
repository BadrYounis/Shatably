using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

public class WorkerConfiguration : IEntityTypeConfiguration<Worker>
{
    public void Configure(EntityTypeBuilder<Worker> builder)
    {
        builder.HasKey(w => w.workerId);

        builder.Property(w => w.workerId)
            .ValueGeneratedOnAdd();

        builder.Property(w => w.fullName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(w => w.email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(w => w.phoneNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(w => w.Type)
            .IsRequired(false)
            .HasConversion<string>() // Store enum as string
            .HasMaxLength(50);

        builder.Property(w => w.ProfilePhotoUrl)
            .HasMaxLength(250);

        builder.HasMany(w => w.RatingReviews)
            .WithOne(rr => rr.Worker)
            .HasForeignKey(rr => rr.WorkerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(w => w.Price).HasColumnType("decimal(18,2)");

        // If you add Schedules in the future:
        // builder.HasMany(w => w.Schedules)
        //        .WithOne(s => s.Worker)
        //        .HasForeignKey(s => s.WorkerId);
    }
}

