namespace Catalog.Products;

public record ProductBrandDefined(Guid Id, Guid BrandId, string BrandName);

public record DefineProductBrand(Guid ProductId, ProductBrand Brand);

public class DefineProductBrandHandler
{
    // TODO
}
