using Ecommerce.Catalog.Products;
using Ecommerce.Domain.Values;

namespace Ecommerce.Catalog.WebApi.Requests;

public record DraftProductRequest(Sku? Sku);

public record EstablishBrandRequest(ProductId? ProductId, BrandId? BrandId);

public record ListTagsRequest(ProductId? ProductId, IReadOnlyList<Tag>? Tags);

public record ConfirmProductRequest(ProductId? ProductId);

public record CancelProductRequest(ProductId? ProductId);
