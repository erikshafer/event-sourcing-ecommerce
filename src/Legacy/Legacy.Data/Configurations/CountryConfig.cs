using Legacy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Legacy.Data.Configurations;

public class CountryConfig : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(e => e.CanBillTo)
            .IsRequired(false)
            .HasDefaultValue(null);

        builder.Property(e => e.CanShipTo)
            .IsRequired(false)
            .HasDefaultValue(null);
    }
}
