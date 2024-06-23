using Legacy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Legacy.Data.Configurations;

public class ListingConfig : IEntityTypeConfiguration<Listing>
{
    public void Configure(EntityTypeBuilder<Listing> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.ItemId)
            .IsRequired();
        // builder.HasOne<Item>(e => e.Item);

        builder.Property(e => e.MarketplaceId)
            .IsRequired();
        // builder.HasOne<Marketplace>(e => e.Marketplace);

        builder.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.IsActiveOnMarketplace)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(e => e.DescriptionShort)
            .IsRequired(false)
            .HasMaxLength(1_000)
            .HasColumnType("nvarchar(1000)");

        builder.Property(e => e.DescriptionLong)
            .IsRequired(false)
            .HasColumnType("nvarchar(max)");

        builder.Property(e => e.BuyItNowPrice)
            .IsRequired(false)
            .HasPrecision(12, 10);

        builder.Property(e => e.StartingPrice)
            .IsRequired(false)
            .HasPrecision(12, 10);

        builder.Property(e => e.ReservePrice)
            .IsRequired(false)
            .HasPrecision(12, 10);

        builder.Property(e => e.StartTime)
            .IsRequired(false);

        builder.Property(e => e.EndTime)
            .IsRequired(false);
    }
}
