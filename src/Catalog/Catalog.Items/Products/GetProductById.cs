using Ecommerce.Core.Exceptions;
using Marten;
using Wolverine.Attributes;

namespace Catalog.Items.Products;

public sealed record GetProductById(Guid ProductId); // query

internal static class GetProductByIdHandler
{
    public static async Task<Product> Handle(GetProductById query, IQuerySession session)
    {
        var product = await session.Events.AggregateStreamAsync<Product>(query.ProductId);

        if (product == null)
            throw AggregateNotFoundException.For<Product>(query.ProductId);

        return product;
    }
}
