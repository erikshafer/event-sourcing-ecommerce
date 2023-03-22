using FluentValidation;

namespace Catalog.Categories.Categories;

public record DraftCategory(Guid CategoryId, string Name, string Code);

public class DraftCategoryValidator : AbstractValidator<DraftCategory>
{
    public DraftCategoryValidator()
    {
        RuleFor(x => x.CategoryId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Code).NotEmpty();
    }
}

// TODO: handler
