namespace Catalog.Items.Categories;

public interface ICategoryValidatorService
{
    Task<bool> Exists(Guid categoryId);
}

public class CategoryValidatorService : ICategoryValidatorService
{
    public async Task<bool> Exists(Guid categoryId)
    {
        return await Task.FromResult(false);
    }
}
