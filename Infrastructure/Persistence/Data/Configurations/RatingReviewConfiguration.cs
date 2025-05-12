using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

public class RatingReviewConfiguration : IEntityTypeConfiguration<RatingReview>
{
    public void Configure(EntityTypeBuilder<RatingReview> builder)
    {
        builder.HasKey(r => r.ReviewId);

        builder.Property(r => r.ReviewId)
            .ValueGeneratedOnAdd();

        builder.Property(r => r.Rating)
               .IsRequired();

        builder.Property(r => r.Comment)
               .HasMaxLength(500);

        builder.Property(r => r.CreatedAt)
               .IsRequired();

        builder.HasOne(r => r.Worker)
               .WithMany(w => w.RatingReviews)
               .HasForeignKey(r => r.WorkerId);

        builder.HasOne(r => r.Client)
               .WithMany(c => c.RatingReviews)
               .HasForeignKey(r => r.ClientId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}