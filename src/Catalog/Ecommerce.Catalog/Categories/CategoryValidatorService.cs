namespace Ecommerce.Catalog.Categories;

public interface ICategoryValidatorService
{
    Task<bool> AlreadyExists(string sku);
}

public class CategoryValidatorService : ICategoryValidatorService
{
    public async Task<bool> AlreadyExists(string sku)
    {
        return await Task.FromResult(false);
    }
}
