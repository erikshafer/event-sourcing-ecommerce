namespace Catalog.Items.Brands;

public interface IBrandValidatorService
{
    Task<bool> Exists(Guid brandId);
}

public class BrandValidatorService : IBrandValidatorService
{
    public async Task<bool> Exists(Guid brandId)
    {
        return await Task.FromResult(false);
    }
}
