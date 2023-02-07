using Ecommerce.Core.Aggregates;

namespace Ecommerce.Catalog.Products;

public class Product : Aggregate
{
    public Sku Sku { get; private set; } = default!;

    public ProductStatus Status { get; private set; }

    public static Product Draft(Guid productId, Sku sku)
    {
        return new Product(productId, sku);
    }

    public Product(){}

    private Product(Guid id, Sku sku)
    {
        var @event = new ProductDrafted(id, sku);
        Enqueue(@event);
        Apply(@event);
    }

    public void Apply(ProductDrafted @event)
    {
        Id = @event.ProductId;
        Sku = @event.Sku;
        Status = ProductStatus.Pending;
    }

    public void Confirm()
    {
        if (Status != ProductStatus.Pending)
            throw new InvalidOperationException($"Cannot confirm product in '{Status}' status");

        var now = DateTime.UtcNow;
        var @event = new ProductConfirmed(Id, now);

        Enqueue(@event);
        Apply(@event);
    }

    private void Apply(ProductConfirmed @event)
    {
        Status = ProductStatus.Confirmed;
    }

    public void Cancel()
    {
        if (Status != ProductStatus.Pending)
            throw new InvalidOperationException($"Cannot cancel product in '{Status}' status");

        var now = DateTime.UtcNow;
        var @event = ProductCancelled.Create(Id, now);

        Enqueue(@event);
        Apply(@event);
    }

    private void Apply(ProductCancelled @event)
    {
        Status = ProductStatus.Cancelled;
    }

    public bool IsReadyToHaveEventBeEmitted() => true;
}
