using Legacy.Monolith.Services;
using Microsoft.AspNetCore.Mvc;

namespace Legacy.Monolith.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemsController(IItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _itemService.GetAll();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _itemService.GetById(id);
        return Ok(result);
    }
}
