namespace Catalog.Products;

public record ProductImageUrlDefined(Guid Id, string ImageUrl);

public record DefineProductImageUrl(Guid Id, ProductImageUrl ImageUrl);

public class DefineProductImageUrlHandler
{
    // TODO
}
