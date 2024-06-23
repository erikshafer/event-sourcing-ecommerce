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

        builder.HasMany<OrderItem>(x => x.OrderItems);

        builder.Property(e => e.InProcess)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.IsCompleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder
            .Property(o => o.PaymentId);

        builder.HasOne<Payment>(x => x.Payment)
            .WithOne(x => x.Order)
            .HasForeignKey<Payment>(x => x.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .Property(o => o.Status)
            .HasConversion<string>()
            .HasMaxLength(24);

        builder.HasOne(o => o.Customer)
            .WithMany()
            .HasForeignKey(o => o.CustomerId);
    }
}
