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

        // inventory histories
        if (dbContext.InventoryHistories.Any() is false)
        {
            var inventoryHistories = GenerateInventoryHistories();
            dbContext.AddRange(inventoryHistories);
            dbContext.SaveChanges();
        }

        // warehouses
        if (dbContext.Warehouses.Any() is false)
        {
            var warehouses = GenerateWarehouses();
            dbContext.AddRange(warehouses);
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

    private static IEnumerable<Warehouse> GenerateWarehouses()
    {
        var date = new DateTime(2019,02,02,00,00,00);

        return new List<Warehouse>
        {
            new() { CreatedOn = date, ModifiedOn = date, Id = 1, Name = "Nebraska", State = "NE" },
            new() { CreatedOn = date, ModifiedOn = date, Id = 2, Name = "Pennsylvania", State = "PA" },
            new() { CreatedOn = date, ModifiedOn = date, Id = 3, Name = "California", State = "CA" },
            new() { CreatedOn = date, ModifiedOn = date, Id = 4, Name = "Nebraska 2", State = "NE" },
            new() { CreatedOn = date, ModifiedOn = date, Id = 5, Name = "Texas", State = "TX" },
            new() { CreatedOn = date, ModifiedOn = date, Id = 6, Name = "New Jersey", State = "NJ" },
            new() { CreatedOn = date, ModifiedOn = date, Id = 7, Name = "California 2", State = "CA" },
            new() { CreatedOn = date, ModifiedOn = date, Id = 8, Name = "Michigan", State = "MI" },
            new() { CreatedOn = date, ModifiedOn = date, Id = 9, Name = "Berwyn Chicago", State = "IL" },
            new() { CreatedOn = date, ModifiedOn = date, Id = 10, Name = "Toronto", State = "ON" },
            new() { CreatedOn = date, ModifiedOn = date, Id = 11, Name = "Vancouver", State = "BC" },
            new() { CreatedOn = date, ModifiedOn = date, Id = 12, Name = "Atlanta", State = "GA" },
            new() { CreatedOn = date, ModifiedOn = date, Id = 13, Name = "Dallas", State = "TX" },
        };
    }
}
