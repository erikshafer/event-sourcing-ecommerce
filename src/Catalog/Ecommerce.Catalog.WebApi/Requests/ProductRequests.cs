using Ecommerce.Domain.Values;

namespace Ecommerce.Catalog.WebApi.Requests;

public record DraftProductRequest(Sku Sku, BrandId BrandId);
