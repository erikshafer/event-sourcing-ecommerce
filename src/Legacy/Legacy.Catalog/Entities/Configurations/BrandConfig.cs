using Legacy.Catalog.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Legacy.Catalog.Entities.Configurations;

public class BrandConfig : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(e => e.PrimaryContactName)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(e => e.PrimaryContactEmail)
            .IsRequired(false)
            .HasMaxLength(100);
    }
}
