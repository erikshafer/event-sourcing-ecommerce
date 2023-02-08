using Ecommerce.Core.Aggregates;

namespace Ecommerce.Catalog.Products;

public class Product : Aggregate
{
    public required Sku Sku { get; init; } = default!;

    public required Brand Brand { get; init; } = default!;

    public required ProductStatus Status { get; set; } = ProductStatus.Unset;

    public Product()
    {
    }

    public Product(ProductDrafted @event)
    {
        Id = @event.ProductId;
        Sku = @event.Sku;
        Brand = @event.Brand;
        Status = ProductStatus.Drafted;
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
