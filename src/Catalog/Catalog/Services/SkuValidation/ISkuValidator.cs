namespace Catalog.Services.SkuValidation;

public interface ISkuValidator
{
    Guid Validate(Sku sku);
}
