using Ecommerce.LegacyCatalog.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.LegacyCatalog.Entities.Configurations;

public class ContentConfig : IEntityTypeConfiguration<Content>
{
    public void Configure(EntityTypeBuilder<Content> builder)
    {
        throw new NotImplementedException();
    }
}
