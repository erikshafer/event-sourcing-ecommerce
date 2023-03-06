namespace Ecommerce.Catalog.Brands;

public interface IBrandValidatorService
{
    Task<bool> AlreadyExists(string sku);
}

public class BrandValidatorService : IBrandValidatorService
{
    public async Task<bool> AlreadyExists(string sku)
    {
        return await Task.FromResult(false);
    }
}
