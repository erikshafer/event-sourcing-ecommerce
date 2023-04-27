namespace Products.Api.Endpoints;

public sealed record DraftProductRequest(string Sku, Guid BrandId, Guid CategoryId);
