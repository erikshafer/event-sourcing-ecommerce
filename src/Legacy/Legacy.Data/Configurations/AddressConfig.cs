using Legacy.Domain.Entities;
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
            .HasMaxLength(128);

        builder.Property(e => e.AddressLine2)
            .IsRequired(false)
            .HasMaxLength(128);

        builder.Property(e => e.City)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(e => e.StateProvince)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(e => e.PostalCode)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(e => e.CountryId);
        builder.HasOne<Country>(e => e.Country)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);;

        builder.Property(e => e.Phone)
            .IsRequired(false)
            .HasMaxLength(64);
    }
}
