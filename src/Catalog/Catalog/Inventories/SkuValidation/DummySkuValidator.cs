namespace Catalog.Inventories.SkuValidation;

public class DummySkuValidator : ISkuValidator
{
    public Guid Validate(Sku sku)
    {
        var productId = Guid.NewGuid();
        return productId;
    }
}
