using Legacy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Legacy.Data.Configurations;

public class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(e => e.ShippingAddressId);
        builder.HasOne<Address>(e => e.ShippingAddress)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);;

        builder.Property(e => e.BillingAddressId);
        builder.HasOne<Address>(e => e.BillingAddress)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);;
    }
}
