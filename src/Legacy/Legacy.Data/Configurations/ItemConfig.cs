using Legacy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Legacy.Data.Configurations;
public class ItemConfig : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(e => e.BrandId);

        builder.HasOne<Brand>(e => e.Brand)
            .WithMany();

        builder.Property(e => e.CategoryId);

        builder.HasOne<Category>(e => e.Category)
            .WithMany();

        builder.Property(e => e.IsVariant)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.IsVariantOf)
            .IsRequired(false);

        builder.Property(e => e.Discontinued);

        builder.Property(e => e.Description)
            .IsRequired();

        builder.Property(e => e.WeightUnit)
            .HasDefaultValue("lbs");

        builder.Property(e => e.Weight)
            .IsRequired(false)
            .HasPrecision(5, 2);

        builder.Property(e => e.MeasureUnit)
            .HasDefaultValue("in");

        builder.Property(e => e.Height)
            .IsRequired(false)
            .HasPrecision(5, 2);

        builder.Property(e => e.Length)
            .IsRequired(false)
            .HasPrecision(5, 2);

        builder.Property(e => e.Width)
            .IsRequired(false)
            .HasPrecision(5, 2);

        builder.Property(e => e.Color)
            .IsRequired(false);

        builder.Property(e => e.Color2)
            .IsRequired(false);

        builder.Property(e => e.BulletPoint1)
            .IsRequired(false);

        builder.Property(e => e.BulletPoint2)
            .IsRequired(false);

        builder.Property(e => e.BulletPoint3)
            .IsRequired(false);

        builder.Property(e => e.WarningCode1)
            .IsRequired(false);

        builder.Property(e => e.WarningCode2)
            .IsRequired(false);

        builder.Property(e => e.WarningCode3)
            .IsRequired(false);

        builder.Property(e => e.ChildCouldChokeWarning)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.Picture1Url)
            .IsRequired(false);

        builder.Property(e => e.Picture2Url)
            .IsRequired(false);

        builder.Property(e => e.Picture3Url)
            .IsRequired(false);

        builder.HasIndex(e => e.Name);

        builder.HasIndex(e => e.CategoryId);

        builder.HasIndex(e => e.BrandId);
    }
}
