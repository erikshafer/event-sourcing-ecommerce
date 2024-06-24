using Legacy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Legacy.Data.Configurations;

public class CartConfig : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.IsLocked)
            .IsRequired(true)
            .HasDefaultValue(false);

        builder.Property(e => e.CustomerId);
        builder.HasOne<Customer>(e => e.Customer);

        builder.Property(e => e.DeliveryAddressId);
        builder.HasOne<Address>(e => e.DeliveryAddress);
    }
}
