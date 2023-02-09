using Ecommerce.Catalog.Products;
using Ecommerce.Core.Aggregates;
using Ecommerce.Domain.Values;

namespace Ecommerce.Catalog.Tests.Builders;

internal class ProductBuilder
{
    private Func<Product> _build = () => new Product();
    private Func<Product, Product>? _modify;

    public ProductBuilder Drafted()
    {
        var productId = new ProductId(Guid.NewGuid());
        var sku = Sku.From("ABC123");
        var brand = new BrandId(10_001);
        var @event = new ProductDrafted(productId, sku, brand);

        var product = new Product(@event);

        _build = () => product;

        return this;
    }

    public ProductBuilder WithEvent(ProductDrafted @event)
    {
        var product = new Product(@event);

        _build = () => product;

        return this;
    }

    public static ProductBuilder Create() => new();

    public Product Build()
    {
        var product = _build();
        _modify?.Invoke(product);
        ((IAggregate)product).DequeueUncommittedEvents();
        return product;
    }
}
