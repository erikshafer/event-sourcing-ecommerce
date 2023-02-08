using Ecommerce.Catalog.Tags;
using Ecommerce.Core.Aggregates;

namespace Ecommerce.Catalog.Products;

public sealed class Product : AggregateWithId<ProductId>
{
    public Sku Sku { get; private set; } = default!;

    public Brand Brand { get; private set; } = default!;

    public ProductStatus Status { get; private set; }

    public IList<ProductTag> Tags { get; private set; } = default!;

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
        Brand = @event.Brand;
        Tags = new List<ProductTag>();
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
