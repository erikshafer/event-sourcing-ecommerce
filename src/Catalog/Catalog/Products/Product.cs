using Ecommerce.Core.Aggregates;

namespace Catalog.Products;

public sealed class Product : Aggregate
{
    public string Sku { get; private set; } = default!;

    public ProductStatus Status { get; private set; }

    public ProductName Name { get; private set; } = ProductName.Blank();

    public Product()
    {
    }

    public Product(ProductDrafted @event)
    {
        Enqueue(@event);
        Apply(@event);
    }

    private Product(Guid productId, string sku)
    {
        var @event = new ProductDrafted(productId, sku);

        Enqueue(@event);
        Apply(@event);
    }

    private void Apply(ProductDrafted @event)
    {
        Id = @event.ProductId;
        Sku = @event.Sku;

        Status = ProductStatus.Drafted;
    }
}
