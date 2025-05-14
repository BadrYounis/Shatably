using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Persistence.Data;
public class ProjectPortfolioConfiguration : IEntityTypeConfiguration<ProjectPortfolio>
{
    public void Configure(EntityTypeBuilder<ProjectPortfolio> builder)
    {
        builder.HasKey(p => p.projectId);

        builder.Property(p => p.projectId)
            .ValueGeneratedOnAdd(); 

        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.description)
            .HasMaxLength(1000); 

        builder.Property(p => p.MainImageUrl)
            .HasMaxLength(250); 

        builder.Property(p => p.createdDate)
            .HasDefaultValueSql("GETDATE()")
            .IsRequired();

        builder.HasMany(p => p.Images)
            .WithOne(i => i.ProjectPortfolio)
            .HasForeignKey(i => i.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
