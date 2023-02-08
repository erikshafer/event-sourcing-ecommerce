using Ecommerce.Core.Exceptions;
using Marten;
using Wolverine.Attributes;

namespace Ecommerce.Catalog.Products;

// query
public sealed record GetProductById(Guid ProductId)
{
    public static GetProductById Create(Guid? productId)
    {
        if (productId == null || productId == Guid.Empty)
            throw new ArgumentOutOfRangeException(nameof(productId));

        return new GetProductById(productId.Value);
    }
}

internal static class GetProductByIdHandler
{
    public static async Task<Product> Handle(GetProductById query, IQuerySession session)
    {
        var product = await session.LoadAsync<Product>(query.ProductId);

        if (product == null)
            throw AggregateNotFoundException.For<Product>(query.ProductId);

        return product;
    }
}
