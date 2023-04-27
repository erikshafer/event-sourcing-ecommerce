using Legacy.Monolith.Entities.Models;

namespace Legacy.Monolith.Services;

public interface IItemService
{
    Task<Item> GetById(int id);
    Task<List<Item>> GetAll();
    Task UpdateName(int id, string name);
    Task UpdateDescription(int id, string description);
    Task UpdateBulletPoints(int id, string[] bulletPoints);
}
