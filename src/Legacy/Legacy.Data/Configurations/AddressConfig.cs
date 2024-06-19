using Legacy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Legacy.Data.Configurations;

public class AddressConfig : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.AddressLine1)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(e => e.AddressLine2)
            .IsRequired(false)
            .HasMaxLength(255);

        builder.Property(e => e.City)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(e => e.CountryId);

        builder.HasOne<Country>(e => e.Country);

        builder.Property(e => e.Phone)
            .IsRequired(false)
            .HasMaxLength(50);
    }
}
