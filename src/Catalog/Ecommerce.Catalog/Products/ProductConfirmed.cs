using Ecommerce.Core.Exceptions;
using Wolverine.Marten;

namespace Ecommerce.Catalog.Products;

public sealed record ProductConfirmed(ProductId ProductId); // event

public sealed record ConfirmProduct(ProductId ProductId); // command

internal static class ProductConfirmedHandler
{
    [MartenCommandWorkflow]
    public static IEnumerable<object> Handle(ConfirmProduct command, Product product)
    {
        if (product.Status != ProductStatus.Drafted)
            throw InvalidAggregateOperationException.For<Product>(product.Id, nameof(ConfirmProduct));

        yield return new ProductConfirmed(product.Id);
    }
}
