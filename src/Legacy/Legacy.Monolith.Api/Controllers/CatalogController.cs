using Legacy.Monolith.DbContexts;
using Microsoft.AspNetCore.Mvc;

namespace Legacy.Monolith.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogController : ControllerBase
{
    private readonly CatalogDbContext _catalogDbContext;

    public CatalogController(CatalogDbContext catalogDbContext)
    {
        _catalogDbContext = catalogDbContext;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _catalogDbContext.GetAllItems();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _catalogDbContext.GetItemById(id);
        return Ok(result);
    }
}
