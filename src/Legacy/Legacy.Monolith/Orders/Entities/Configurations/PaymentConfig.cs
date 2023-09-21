using Legacy.Monolith.Catalog.Entities.Models;
using Legacy.Monolith.Orders.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Legacy.Monolith.Orders.Entities.Configurations;

public class PaymentConfig : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne<Order>(x => x.Order)
            .WithOne(x => x.Payment)
            .HasForeignKey<Payment>(x => x.OrderId);

        builder.Property(e => e.Completed)
            .IsRequired()
            .HasDefaultValue(false);
    }
}
