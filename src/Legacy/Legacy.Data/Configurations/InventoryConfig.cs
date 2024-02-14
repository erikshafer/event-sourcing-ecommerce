using Legacy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Legacy.Data.Configurations;

public class InventoryConfig : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Available)
            .IsRequired();
    }
}
