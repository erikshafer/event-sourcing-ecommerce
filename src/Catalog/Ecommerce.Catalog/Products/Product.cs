using Ecommerce.Core.Aggregates;
using Ecommerce.Domain.Values;

namespace Ecommerce.Catalog.Products;

public sealed class Product : AggregateWithId<ProductId>
{
    public Sku Sku { get; private set; } = default!;

    public BrandId BrandId { get; private set; } = default!;

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

    private void Apply(ProductDrafted @event)
    {
        Id = @event.ProductId;
        Sku = @event.Sku;
        BrandId = @event.BrandId;
        Tags = new List<Tag>();
        Status = ProductStatus.Drafted;
    }

    private void Apply(ProductConfirmed @event)
    {
        Status = ProductStatus.Confirmed;
    }

    private void Apply(ProductCancelled @event)
    {
        Status = ProductStatus.Cancelled;
    }

    // TODO
    // private void AddTag(ProductTag productTag)
    // {
    //
    // }
}
