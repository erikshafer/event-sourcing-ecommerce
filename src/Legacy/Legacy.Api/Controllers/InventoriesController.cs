using Legacy.Application.Services.Inventory;
using Legacy.Data.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Legacy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoriesController : ControllerBase
{
    private readonly InventoryDbContext _dbContext;
    private readonly IInventoryService _service;

    public InventoriesController(InventoryDbContext dbContext, IInventoryService service)
    {
        _dbContext = dbContext;
        _service = service;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await _dbContext.Inventories
            .AsNoTracking()
            .ToListAsync(ct);
        return Ok(result);
    }

    [HttpGet("{itemId:int}")]
    public async Task<IActionResult> GetInventoryOfItemFromAllWarehouses(int itemId, CancellationToken ct)
    {
        var result = await _dbContext.Inventories
            .AsNoTracking()
            .Where(i => i.ItemId == itemId)
            .ToListAsync(ct);
        return Ok(result);
    }

    [HttpGet("{itemId:int}/{warehouseId:int}")]
    public async Task<IActionResult> GetInventoryOfItemFromWarehouse(int itemId, int warehouseId, CancellationToken ct)
    {
        var result = await _dbContext.Inventories
            .AsNoTracking()
            .Where(i => i.ItemId == itemId && i.WarehouseId == warehouseId)
            .ToListAsync(ct);
        return Ok(result);
    }

    [HttpGet("/update-stock")]
    public async Task<IActionResult> UpdateStock([FromBody] UpdateStockRequest request, CancellationToken ct)
    {
        await _service.UpdateStock(request);
        var result = await _dbContext.Inventories
            .AsNoTracking()
            .Where(i => i.ItemId == request.ItemId && i.WarehouseId == request.WarehouseId)
            .ToListAsync(ct);
        return Ok(result);
    }
}
