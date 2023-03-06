using Ecommerce.Core.Exceptions;
using Marten;
using Wolverine.Attributes;

namespace Ecommerce.Catalog.Products;

public record GetProductAtVersion(Guid ProductId, long Version); // query

internal static class GetProductAtVersionHandler
{
    public static async Task<Product> Handle(GetProductAtVersion query, IQuerySession session)
    {
        var (productId, version) = query;
        return await session.Events.AggregateStreamAsync<Product>(productId, version)
               ?? throw AggregateNotFoundException.For<Product>(productId);
    }
}
