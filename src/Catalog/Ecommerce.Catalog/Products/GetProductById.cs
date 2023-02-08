using Ecommerce.Core.Exceptions;
using Marten;
using Wolverine.Attributes;

namespace Ecommerce.Catalog.Products;

public sealed record GetProductById(Guid ProductId); // query; present tense

internal static class GetProductByIdHandler
{
    [Transactional]
    public static async Task<Product> Handle(GetProductById query, IQuerySession session)
    {
        var product = await session.LoadAsync<Product>(query.ProductId);
        
        if (product == null)
            throw AggregateNotFoundException.For<Product>(query.ProductId);
        
        return product;
    }
}