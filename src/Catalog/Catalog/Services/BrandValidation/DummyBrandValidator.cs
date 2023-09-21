namespace Catalog.Services.BrandValidation;

public class DummyBrandValidator : IBrandValidator
{
    public Guid Validate(Guid brandId)
    {
        var id = Guid.NewGuid();
        return id;
    }

    public Guid Validate(int legacyBrandId)
    {
        var id = Guid.NewGuid();
        return id;
    }
}
