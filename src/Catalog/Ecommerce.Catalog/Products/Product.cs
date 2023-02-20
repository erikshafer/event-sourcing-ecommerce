using Ecommerce.Core.Aggregates;
using Ecommerce.Core.Extensions;
using Ecommerce.Domain.Values;

namespace Ecommerce.Catalog.Products;

public sealed class Product : Aggregate
{
    public string Sku { get; private set; } = default!;

    public int BrandId { get; private set; }

    public ProductStatus Status { get; private set; }

    public IList<Tag> Tags { get; private set; } = default!;

    public Product()
    {
    }

    public Product(ProductDrafted @event)
    {
        Enqueue(@event);
        Apply(@event);
    }

    public void Apply(ProductDrafted @event)
    {
        Id = @event.ProductId;
        Sku = @event.Sku;
        Tags = new List<Tag>();
        Status = ProductStatus.Drafted;
        BrandId = -1;
    }

    public void Apply(BrandEstablished @event)
    {
        BrandId = @event.BrandId;
    }

    public void Apply(TagsListed @event)
    {
        Tags.ClearAndReplace(@event.Tags);
    }

    public void Apply(ProductConfirmed @event)
    {
        Status = ProductStatus.Confirmed;
    }

    public void Apply(ProductCancelled @event)
    {
        Status = ProductStatus.Cancelled;
    }
}
