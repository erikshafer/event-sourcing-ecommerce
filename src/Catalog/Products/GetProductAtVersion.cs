using Ecommerce.Core.Exceptions;
using FluentValidation;
using Marten;

namespace Products;

public record GetProductAtVersion(Guid ProductId, long Version); // query

public class GetProductAtVersionValidator : AbstractValidator<GetProductAtVersion>
{
    public GetProductAtVersionValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Version).NotEmpty(); // no zeros
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
