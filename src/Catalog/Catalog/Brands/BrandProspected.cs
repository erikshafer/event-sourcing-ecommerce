namespace Catalog.Brands;

public record BrandProspected(Guid BrandId, string Name);

public record ProspectBrand(Guid BrandId, BrandName Name);

// TODO: handler
