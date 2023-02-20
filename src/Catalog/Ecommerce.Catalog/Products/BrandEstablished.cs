using Ecommerce.Core.Exceptions;
using Wolverine.Marten;

namespace Ecommerce.Catalog.Products;

public record BrandEstablished(Guid ProductId, int BrandId); // event

public record EstablishBrand(Guid ProductId, int BrandId); // command

internal static class EstablishBrandHandler
{
    [MartenCommandWorkflow]
    public static IEnumerable<object> Handle(EstablishBrand command, Product product)
    {
        if (product.Status != ProductStatus.Drafted)
            throw InvalidAggregateOperationException.For<Product>(product.Id, nameof(EstablishBrand));

        yield return new ProductConfirmed(product.Id);
    }
}
