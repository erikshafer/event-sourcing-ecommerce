using Ecommerce.Core.Exceptions;
using Wolverine.Marten;

namespace Ecommerce.Catalog.Products;

public record ProductCancelled(Guid ProductId); // event

public record CancelProduct(Guid ProductId); // command

internal static class CancelProductHandler
{
    [MartenCommandWorkflow]
    public static IEnumerable<object> Handle(CancelProduct command, Product product)
    {
        if (product.Status != ProductStatus.Drafted)
            throw InvalidAggregateOperationException.For<Product>(product.Id, nameof(CancelProduct));

        yield return new ProductCancelled(product.Id);
    }
}
