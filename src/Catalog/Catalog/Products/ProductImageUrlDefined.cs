using Wolverine.Attributes;

namespace Catalog.Products;

public record ProductImageUrlDefined(Guid Id, string ImageUrl);

public record DefineProductImageUrl(Guid Id, ProductImageUrl ImageUrl);

[WolverineHandler]
public class DefineProductImageUrlHandler
{
    // TODO
}
