using Ecommerce.Catalog.Products;
using Ecommerce.Catalog.Tags;
using Ecommerce.Core.Aggregates;

namespace Ecommerce.Catalog.Tests.Builders;

internal class ProductBuilder
{
    private Func<Product> _build = () => new Product();
    private Func<Product, Product>? _modify;

    public ProductBuilder Drafted()
    {
        var productId = new ProductId(Guid.NewGuid());
        var sku = new Sku("ABC123");
        var brand = new Brand(10_001, "Acme Inc");
        var name = "Instant Tunnel";
        var @event = new ProductDrafted(productId, sku, brand, name);

        var product = new Product(@event);

        _build = () => product;

        return this;
    }

    // TODO
    // public ProductBuilder WithTag()
    // {
    //     var productId = Guid.NewGuid();
    //     const string tagValue = "instant";
    //     const int tagOrdering = 1;
    //     var productTag = new ProductTag(tagValue, tagOrdering);
    //
    //     modify += product =>
    //     {
    //         product.AddTag(productTag);
    //         return product;
    //     };
    //
    //     return this;
    // }

    public static ProductBuilder Create() => new();

    public Product Build()
    {
        var product = _build();
        _modify?.Invoke(product);
        ((IAggregate)product).DequeueUncommittedEvents();
        return product;
    }
}
