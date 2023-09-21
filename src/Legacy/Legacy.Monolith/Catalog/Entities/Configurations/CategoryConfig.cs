using Legacy.Monolith.Catalog.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Legacy.Monolith.Catalog.Entities.Configurations;

public class CategoryConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(8);

        builder.HasIndex(e => e.Code);

        builder.Property(e => e.Description)
            .IsRequired(false)
            .HasMaxLength(128);

        builder.Property(e => e.ParentId)
            .IsRequired(false);

        builder.HasIndex(e => e.ParentId);
    }
}
