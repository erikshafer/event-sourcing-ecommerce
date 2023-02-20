using Ecommerce.Core.Exceptions;
using Marten;
using Wolverine.Attributes;

namespace Ecommerce.Catalog.Products;

// query
public sealed record GetProductAtVersion(Guid ProductId, long Version)
{
    public static GetProductAtVersion Create(Guid? productId, long? version)
    {
        if (productId == null || productId == Guid.Empty)
            throw new ArgumentOutOfRangeException(nameof(productId));
        if (version is null or < 0)
            throw new ArgumentOutOfRangeException(nameof(version));

        return new GetProductAtVersion(productId.Value, version.Value);
    }
}

internal static class GetProductAtVersionHandler
{
    public static async Task<Product> Handle(GetProductAtVersion query, IQuerySession session)
    {
        var (productId, version) = query;
        return await session.Events.AggregateStreamAsync<Product>(productId, version)
               ?? throw AggregateNotFoundException.For<Product>(productId);
    }
}
