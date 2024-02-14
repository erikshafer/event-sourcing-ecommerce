using Legacy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Legacy.Data.Configurations;

public class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.TotalPrice)
            .IsRequired(true)
            .HasPrecision(10, 2);

        builder.HasMany<Item>(x => x.Items);

        builder.Property(e => e.Completed)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasOne<Payment>(x => x.Payment)
            .WithOne(x => x.Order)
            .HasForeignKey<Payment>(x => x.OrderId);
    }
}
