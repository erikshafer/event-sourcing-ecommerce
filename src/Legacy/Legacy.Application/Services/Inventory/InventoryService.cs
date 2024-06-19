using Legacy.Application.Events.Inventory;
using Legacy.Data.DbContexts;
using Legacy.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Legacy.Application.Services.Inventory;

public class InventoryService : IInventoryService
{
    private readonly InventoryDbContext _inventoryDbContext;
    private readonly CatalogDbContext _catalogDbContext;
    private readonly IMediator _mediator;

    public InventoryService(InventoryDbContext inventoryDbContext, CatalogDbContext catalogDbContext, IMediator mediator)
    {
        _inventoryDbContext = inventoryDbContext;
        _catalogDbContext = catalogDbContext;
        _mediator = mediator;
    }

    public async Task UpdateStock(UpdateStockRequest request)
    {
        var item = await _catalogDbContext.Items.FirstOrDefaultAsync(x =>
            x.Id == request.ItemId);

        if (item is null)
            throw new Exception($"Item '{request.ItemId}' not found");

        var inventory = await _inventoryDbContext.Inventories.FirstOrDefaultAsync(x =>
            x.ItemId == request.ItemId &&
            x.WarehouseId == request.WarehouseId);

        if (inventory is null)
            throw new Exception($"Inventory '{request.ItemId}' not found");

        var adjustedQuantity = request.AdjustedQuantity;
        var previousQuantity = inventory.Quantity;
        var newQuantity = previousQuantity + adjustedQuantity;
        inventory.Quantity = newQuantity;

        var now = DateTime.Now;
        var inventoryHistory = new InventoryHistory
        {
            CreatedOn = now,
            ModifiedOn = now,
            ItemId = item.Id,
            WarehouseId = inventory.WarehouseId,
            AdjustedQuantity = adjustedQuantity,
            ReportedTotal = inventory.Quantity,
            Note = request.Note
        };

        _inventoryDbContext.InventoryHistories.Add(inventoryHistory);
        await _inventoryDbContext.SaveChangesAsync();

        if (previousQuantity <= 0 && inventory.Quantity > 0)
            await _mediator.Publish(new ItemBackInStock(item.Id));
    }
}
