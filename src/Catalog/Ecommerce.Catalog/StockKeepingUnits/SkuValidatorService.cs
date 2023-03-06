namespace Ecommerce.Catalog.StockKeepingUnits;

public interface ISkuValidatorService
{
    Task<bool> AlreadyExists(string sku);
}

public class SkuValidatorService : ISkuValidatorService
{
    public async Task<bool> AlreadyExists(string sku)
    {
        return await Task.FromResult(false);
    }
}
