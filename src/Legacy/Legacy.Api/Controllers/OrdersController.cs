using Legacy.Data.DbContexts;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> GetAll()
    {
        var result = await _orderingDbContext.GetAllOrders();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _orderingDbContext.GetOrderById(id);
        return Ok(result);
    }
}
