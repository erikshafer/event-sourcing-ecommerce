using Ecommerce.LegacyCatalog.DbContexts;
using Ecommerce.LegacyCatalog.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.LegacyCatalog.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestrictionsController : ControllerBase
{
    private readonly LegacyCatalogDbContext _dbContext;

    public RestrictionsController(LegacyCatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _dbContext.Restrictions.ToListAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _dbContext.Restrictions.FirstOrDefaultAsync(x => x.Id == id);
        return Ok(result);
    }
}
