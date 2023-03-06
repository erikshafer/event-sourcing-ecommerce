namespace Ecommerce.Catalog.Products;

public record BrandAdjusted(Guid ProductId, Guid BrandId);

public record AdjustBrand(Guid ProductId, Guid BrandId);
