using FluentValidation;

namespace Catalog.Products;

public record ProductDrafted(Guid ProductId, string Sku);

public record DraftProduct(Guid ProductId, string Sku);

public class DraftProductValidator : AbstractValidator<DraftProduct>
{
    public DraftProductValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Sku).NotEmpty().MaximumLength(16);
    }
}

public class DraftProductHandler
{
    public static void Handle(DraftProduct command)
    {

    }
}
