using Legacy.Monolith.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Legacy.Monolith.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly CatalogDbContext _dbContext;

    public ItemsController(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _dbContext.Items.ToListAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _dbContext.Items.FirstOrDefaultAsync(x => x.Id == id);
        return Ok(result);
    }
}
