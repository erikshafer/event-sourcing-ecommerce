using Legacy.Monolith.Catalog.Entities.Models;
using Legacy.Monolith.Orders.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Legacy.Monolith.Orders.Entities.Configurations;

public class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.TotalPrice)
            .IsRequired();

        builder.HasMany<Item>(x => x.Items);

        builder.Property(e => e.Completed)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasOne<Payment>(x => x.Payment)
            .WithOne(x => x.Order)
            .HasForeignKey<Payment>(x => x.OrderId);
    }
}
