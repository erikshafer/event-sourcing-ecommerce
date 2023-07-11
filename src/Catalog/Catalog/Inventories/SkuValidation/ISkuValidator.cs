namespace Catalog.Inventories.SkuValidation;

public interface ISkuValidator
{
    Guid Validate(Sku sku);
}
