using Ecommerce.LegacyCatalog.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.LegacyCatalog.Entities.Configurations;

public class RestrictionConfig : IEntityTypeConfiguration<Restriction>
{
    public void Configure(EntityTypeBuilder<Restriction> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.ItemId)
            .IsRequired();

        builder.Property(e => e.IsRestricted)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(e => e.Reason)
            .IsRequired(false);
    }
}
