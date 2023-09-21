namespace Catalog.Services.BrandValidation;

public interface IBrandValidator
{
    Guid Validate(Guid brandId);

    Guid Validate(int legacyBrandId);
}
