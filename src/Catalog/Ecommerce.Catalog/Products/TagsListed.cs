using Ecommerce.Core.Exceptions;
using Ecommerce.Domain.Values;
using Wolverine.Marten;

namespace Ecommerce.Catalog.Products;

public record TagsListed(Guid ProductId, IReadOnlyList<Tag> Tags); //event

public record ListTags(Guid ProductId, IReadOnlyList<Tag> Tags); //command

internal static class ListTagsHandler
{
    [MartenCommandWorkflow]
    public static IEnumerable<object> Handle(ListTags command, Product product)
    {
        if (product.Status != ProductStatus.Drafted)
            throw InvalidAggregateOperationException.For<Product>(product.Id, nameof(ListTags));

        if (command.Tags.Count > 5)
            throw InvalidAggregateOperationException.Because<Product>(product.Id, nameof(ListTags), $"Exceeded the five (5) tag limit with {command.Tags.Count}");

        yield return new TagsListed(product.Id, command.Tags);
    }
}
