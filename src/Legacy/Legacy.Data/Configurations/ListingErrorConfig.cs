using Legacy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Legacy.Data.Configurations;

public class ListingErrorConfig : IEntityTypeConfiguration<ListingError>
{
    public void Configure(EntityTypeBuilder<ListingError> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.ListingId)
            .IsRequired();

        builder.Property(e => e.ItemId)
            .IsRequired();

        builder.Property(e => e.MarketplaceId)
            .IsRequired();

        builder.Property(e => e.ErrorId)
            .IsRequired(false)
            .HasMaxLength(500)
            .HasColumnType("varchar(500)");

        builder.Property(e => e.ErrorMessage)
            .IsRequired(false)
            .HasColumnType("nvarchar(max)");

        builder.Property(e => e.WarningId)
            .IsRequired(false)
            .HasMaxLength(500)
            .HasColumnType("varchar(500)");

        builder.Property(e => e.WarningMessage)
            .IsRequired(false)
            .HasColumnType("nvarchar(max)");
    }
}
