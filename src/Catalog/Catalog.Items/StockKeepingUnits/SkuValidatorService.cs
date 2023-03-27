namespace Catalog.Items.StockKeepingUnits;

public interface ISkuValidatorService
{
    Task<bool> Exists(string sku);
}

public class SkuValidatorService : ISkuValidatorService
{
    public async Task<bool> Exists(string sku)
    {
        return await Task.FromResult(false);
    }
}
