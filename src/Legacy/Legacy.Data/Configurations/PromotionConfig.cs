using Legacy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Legacy.Data.Configurations;

public class PromotionConfig : IEntityTypeConfiguration<Promotion>
{
    public void Configure(EntityTypeBuilder<Promotion> builder)
    {
        builder.HasKey(e => e.Id);


        builder.Property(e => e.ClaimedByCustomerId);
        builder.HasOne<Customer>(e => e.ClaimedByCustomer);
    }
}
