using Legacy.Data.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Legacy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogController : ControllerBase
{
    private readonly CatalogDbContext _dbContext;

    public CatalogController(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await _dbContext.Items.ToListAsync(ct);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var result = await _dbContext.Items.FirstOrDefaultAsync(item => item.Id == id, ct);
        return result == null ? NotFound() : Ok(result);
    }
}
