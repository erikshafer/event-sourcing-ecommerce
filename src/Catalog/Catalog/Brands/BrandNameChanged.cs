namespace Catalog.Brands;

public record BrandNameChanged(Guid BrandId, string Name);

public record ChangeBrandName(Guid BrandId, BrandName Name);

// TODO: handler
