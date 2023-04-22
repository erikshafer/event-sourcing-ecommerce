using Legacy.Catalog.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Legacy.Catalog.Services;

public class ItemService
{
    private readonly LegacyCatalogDbContext _dbContext;

    public ItemService(LegacyCatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task UpdateName(int id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new Exception();
        }

        var item = await _dbContext.Items.FirstOrDefaultAsync(x => x.Id == id);

        if (item == null)
        {
            throw new Exception();
        }

        item.Name = name;
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateDescription(int id, string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new Exception();
        }

        var item = await _dbContext.Items.FirstOrDefaultAsync(x => x.Id == id);

        if (item == null)
        {
            throw new Exception();
        }

        item.Name = description;
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateBulletPoints(int id, string[] bulletPoints)
    {
        if (!bulletPoints.Any())
        {
            throw new Exception();
        }

        var item = await _dbContext.Items.FirstOrDefaultAsync(x => x.Id == id);

        if (item == null)
        {
            throw new Exception();
        }

        item.BulletPoint1 = bulletPoints[0];
        item.BulletPoint2 = bulletPoints[1];
        item.BulletPoint3 = bulletPoints[2];

        await _dbContext.SaveChangesAsync();
    }
}
