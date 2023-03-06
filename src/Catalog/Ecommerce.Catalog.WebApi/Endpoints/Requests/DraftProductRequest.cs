namespace Ecommerce.Catalog.WebApi.Endpoints.Requests;

public sealed record DraftProductRequest(string Sku, Guid BrandId, Guid CategoryId);
