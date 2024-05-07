using Legacy.Data.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Legacy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrderingDbContext _orderingDbContext;

    public OrdersController(OrderingDbContext orderingDbContext)
    {
        _orderingDbContext = orderingDbContext;
    }
    [HttpGet("")]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await _orderingDbContext.Orders.ToListAsync(ct);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var result = await _orderingDbContext.Orders.FirstOrDefaultAsync(x => x.Id == id, ct);
        return result == null ? NotFound() : Ok(result);
    }
}
