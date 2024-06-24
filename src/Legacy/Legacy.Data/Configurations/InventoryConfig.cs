using Legacy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Legacy.Data.Configurations;

public class InventoryConfig : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.ToTable("Inventories");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.ItemId);
        // builder.HasOne<Item>(e => e.Item)
        //     .WithMany()
        //     .OnDelete(DeleteBehavior.Restrict);

        builder.Property(e => e.WarehouseId);
        // builder.HasOne<Warehouse>(e => e.Warehouse)
        //     .WithMany()
        //     .OnDelete(DeleteBehavior.Restrict);

        builder.Property(e => e.Quantity)
            .IsRequired()
            .HasDefaultValue(0);

        builder.HasIndex(e => new { e.ItemId, e.WarehouseId })
            .IsUnique();

    }
}
