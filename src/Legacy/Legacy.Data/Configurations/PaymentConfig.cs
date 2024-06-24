using Legacy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Legacy.Data.Configurations;

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

        builder.Property(e => e.CustomerId);
        builder.HasOne<Customer>(e => e.Customer);

        builder.Property(e => e.TotalAmount)
            .IsRequired()
            .HasPrecision(12, 10);

    }
}
