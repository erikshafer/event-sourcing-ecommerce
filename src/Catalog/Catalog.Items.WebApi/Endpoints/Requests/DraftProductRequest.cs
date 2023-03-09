namespace Catalog.Items.WebApi.Endpoints.Requests;

public sealed record DraftProductRequest(string Sku, Guid BrandId, Guid CategoryId);
