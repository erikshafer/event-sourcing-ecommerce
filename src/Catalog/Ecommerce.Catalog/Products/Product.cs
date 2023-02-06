using Ecommerce.Catalog.Products.Cancelling;
using Ecommerce.Catalog.Products.Confirming;
using Ecommerce.Catalog.Products.Drafting;

namespace Ecommerce.Catalog.Products;

public class Product
{
    public Guid Id { get; private set; }

    public Sku Sku { get; private set; } = default!;

    public ProductStatus Status { get; private set; }

    public static Product Draft(Guid productId, Sku sku)
    {
        return new Product(productId, sku);
    }

    public Product(){}
    
    private Product(Guid id, Sku sku)
    {
        var @event = ProductDrafted.Create(id, sku);
        
        // TODO - enqueue
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
        var @event = ProductConfirmed.Create(Id, now);
        
        // TODO - enqueue
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
        
        // TODO - enqueue
        Apply(@event);
    }

    private void Apply(ProductCancelled @event)
    {
        Status = ProductStatus.Cancelled;
    }
}