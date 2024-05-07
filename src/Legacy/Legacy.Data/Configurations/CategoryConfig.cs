using Legacy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Legacy.Data.Configurations;

public class CategoryConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(24);

        builder.HasIndex(e => e.Code);

        builder.Property(e => e.Description)
            .IsRequired(false)
            .HasMaxLength(1024);

        builder.Property(e => e.ParentId)
            .IsRequired(false);

        builder.HasIndex(e => e.ParentId);
    }
}
