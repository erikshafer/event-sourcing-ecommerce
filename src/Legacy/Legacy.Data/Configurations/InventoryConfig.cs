using Legacy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Legacy.Data.Configurations;

public class InventoryConfig : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.ItemId);

        builder.HasOne<Item>(e => e.Item);

        builder.Property(e => e.WarehouseId);

        builder.HasOne<Warehouse>(e => e.Warehouse);

        builder.Property(e => e.Quantity)
            .IsRequired()
            .HasDefaultValue(0);
    }
}
