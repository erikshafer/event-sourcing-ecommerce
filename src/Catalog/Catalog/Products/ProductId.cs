using Ecommerce.Core.Identities;

namespace Catalog.Products;

public sealed record ProductId : Id
{
    private ProductId(string value)
        : base(value) { }

    public static ProductId Empty() => new(string.Empty);
}
