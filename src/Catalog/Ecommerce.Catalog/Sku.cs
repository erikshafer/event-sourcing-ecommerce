namespace Ecommerce.Catalog;

public record Sku(string Value);

public record ProductSku(Guid ProductId, Sku Sku);
