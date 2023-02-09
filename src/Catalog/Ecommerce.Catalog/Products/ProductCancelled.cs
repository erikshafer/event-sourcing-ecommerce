using Ecommerce.Core.Exceptions;
using Wolverine.Marten;

namespace Ecommerce.Catalog.Products;

public record ProductCancelled(ProductId ProductId); // event

public record CancelProduct(ProductId ProductId); // command

internal static class CancelProductHandler
{
    [MartenCommandWorkflow]
    public static IEnumerable<object> Handle(CancelProduct command, Product product)
    {
        if (product.Status != ProductStatus.Drafted)
            throw InvalidAggregateOperationException.For<Product>(product.Id, nameof(CancelProduct));

        yield return new ProductCancelled(new ProductId(product.Id));
    }
}
