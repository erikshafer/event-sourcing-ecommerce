using Ecommerce.Core.Exceptions;
using Wolverine.Marten;

namespace Ecommerce.Catalog.Products;

public record BrandEstablished(ProductId ProductId, BrandId BrandId); // event

public record EstablishBrand(ProductId ProductId, BrandId BrandId); // command

internal static class EstablishBrandHandler
{
    [MartenCommandWorkflow]
    public static IEnumerable<object> Handle(EstablishBrand command, Product product)
    {
        if (product.Status != ProductStatus.Drafted)
            throw InvalidAggregateOperationException.For<Product>(product.Id, nameof(EstablishBrand));

        yield return new ProductConfirmed(new ProductId(product.Id));
    }
}
