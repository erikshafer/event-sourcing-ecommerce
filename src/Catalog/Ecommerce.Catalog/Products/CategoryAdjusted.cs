namespace Ecommerce.Catalog.Products;

public record CategoryAdjusted(Guid ProductId, Guid CategoryId);

public record AdjustCategory(Guid ProductId, Guid CategoryId);
