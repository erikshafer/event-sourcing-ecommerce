using Ecommerce.Core.Exceptions;
using Ecommerce.Domain.Values;
using FluentValidation;
using Wolverine.Attributes;
using Wolverine.Marten;

namespace Ecommerce.Catalog.Products;

public record TagsListed(Guid ProductId, IReadOnlyList<Tag> Tags); //event

public record ListTags(Guid ProductId, IReadOnlyList<Tag> Tags); //command

public class ListTagsValidator : AbstractValidator<ListTags>
{
    public ListTagsValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Tags).NotEmpty();
    }
}

[WolverineHandler]
public static class ListTagsHandler
{
    [MartenCommandWorkflow]
    public static IEnumerable<object> Handle(ListTags command, Product product)
    {
        if (product.Status != ProductStatus.Drafted)
            throw InvalidAggregateOperationException.For<Product>(product.Id, nameof(ListTags));

        if (command.Tags.Count > 5)
            throw InvalidAggregateOperationException.Because<Product>(
                product.Id, nameof(ListTags), $"Exceeded the five (5) tag limit with {command.Tags.Count}");

        yield return new TagsListed(product.Id, command.Tags);
    }
}
