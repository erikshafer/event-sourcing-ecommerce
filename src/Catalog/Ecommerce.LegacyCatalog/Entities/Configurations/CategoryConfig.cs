using Ecommerce.LegacyCatalog.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.LegacyCatalog.Entities.Configurations;

public class CategoryConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(64);
    }
}