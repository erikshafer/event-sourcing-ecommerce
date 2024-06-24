using Bogus;
using Legacy.Data.DbContexts;
using Legacy.Domain.Entities;

namespace Legacy.Data.Seeds;

public static class InventorySeeder
{
    private const int MaxInventories = 1_024;
    private const int MaxInventoryHistories = 1_024;
    private const int MaxWarehouses = 13;
    private const int MaxQuantityOnHand = 24;
    private const float ChanceOfOutOfStock = 0.15f;

    private const int MaxBrands = 64;
    private const int MaxCategories = 512;
    private const int MaxItems = 1_024;
    private const int MaxRestrictions = 128;

    public static IEnumerable<Inventory> GenerateInventories()
    {
        var id = 0;
        var inventoryFaker = new Faker<Inventory>()
            .UseSeed(1_000)
            .RuleFor(p => p.Id, f => ++id)
            .RuleFor(p => p.ItemId, f => f.Random.Number(1, MaxItems))
            .Ignore(p => p.Item)
            .RuleFor(p => p.WarehouseId, f => f.Random.Number(1, MaxWarehouses))
            .Ignore(p => p.Warehouse)
            .RuleFor(p => p.Quantity, f => f.Random.Number(1, MaxQuantityOnHand).OrDefault(f, ChanceOfOutOfStock));
        var inventories = inventoryFaker.Generate(MaxInventories);
        return inventories;
    }

    public static IEnumerable<InventoryHistory> GenerateInventoryHistories()
    {
        // TODO: more comprehensive inventory history faker rules
        var id = 0;
        var inventoryHistoriesFaker = new Faker<InventoryHistory>()
            .UseSeed(1_000)
            .RuleFor(p => p.Id, f => ++id);
        var inventoryHistories = inventoryHistoriesFaker.Generate(MaxInventories);
        return inventoryHistories;
    }

    public static IEnumerable<Warehouse> GenerateWarehouses()
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
