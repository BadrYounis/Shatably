using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;
public class SelectedDesignConfiguration : IEntityTypeConfiguration<SelectedDesign>
{
    public void Configure(EntityTypeBuilder<SelectedDesign> builder)
    {
        builder.HasKey(e => e.SelectedDesignId);

        builder.HasOne(e => e.Order)
            .WithMany(o => o.SelectedDesigns)
            .HasForeignKey(e => e.OrderId);

        builder.HasOne(e => e.DesignTemplate)
            .WithMany(dt => dt.SelectedDesigns) 
            .HasForeignKey(e => e.DesignTemplateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.ProjectPortfolio)
            .WithMany()
            .HasForeignKey(e => e.ProjectPortfolioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}