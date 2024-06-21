using Bogus;
using Legacy.Data.DbContexts;
using Legacy.Data.Entities;

namespace Legacy.Data.Seeds;

public static class InventorySeeder
{
    private const int MaxInventories = 1_024;
    private const int MaxInventoryHistories = 1_024;

    public static void Seed(this InventoryDbContext dbContext)
    {
        // inventories
        if (dbContext.Inventories.Any() is false)
        {
            var inventories = GenerateInventories();
            dbContext.AddRange(inventories);
            dbContext.SaveChanges();
        }

        // inventories
        if (dbContext.InventoryHistories.Any() is false)
        {
            var inventoryHistories = GenerateInventoryHistories();
            dbContext.AddRange(inventoryHistories);
            dbContext.SaveChanges();
        }
    }

    private static IEnumerable<Inventory> GenerateInventories()
    {
        var inventoryFaker = new Faker<Inventory>();
        // TODO: inventory faker rules
        var inventories = inventoryFaker.Generate(MaxInventories);
        return inventories;
    }

    private static IEnumerable<InventoryHistory> GenerateInventoryHistories()
    {
        var inventoryHistoriesFaker = new Faker<InventoryHistory>();
        // TODO: inventory history faker rules
        var inventoryHistories = inventoryHistoriesFaker.Generate(MaxInventories);
        return inventoryHistories;
    }
}
