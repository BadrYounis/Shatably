using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;
public class DesignTemplateConfiguration : IEntityTypeConfiguration<DesignTemplate>
{
    public void Configure(EntityTypeBuilder<DesignTemplate> builder)
    {
        builder.HasKey(dt => dt.templateId);

        builder.Property(dt => dt.templateId)
            .ValueGeneratedOnAdd();

        builder.Property(dt => dt.name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(dt => dt.category)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(dt => dt.imageUrl)
               .IsRequired();

        builder.Property(dt => dt.description)
               .HasMaxLength(500);
    }
}
