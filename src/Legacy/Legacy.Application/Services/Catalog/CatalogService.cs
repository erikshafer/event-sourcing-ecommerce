using Legacy.Application.Events.Events;
using Legacy.Data.DbContexts;
using Legacy.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Legacy.Application.Services.Catalog;

public class CatalogService : ICatalogService
{
    private readonly CatalogDbContext _catalogDbContext;
    private readonly IMediator _mediator;

    public CatalogService(CatalogDbContext catalogDbContext, IMediator mediator)
    {
        _catalogDbContext = catalogDbContext;
        _mediator = mediator;
    }

    public async Task CreateNewItem(NewItemRequest request)
    {
        var item = await _catalogDbContext.Items
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.ItemId);

        if (item is not null)
            throw new Exception($"Item '{request.ItemId}' already exists -- cannot create new");

        var brand = await _catalogDbContext.Brands
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.BrandId);

        if (brand is null)
            throw new Exception($"Brand '{request.BrandId}' not found");

        var category = await _catalogDbContext.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.CategoryId);

        if (category is null)
            throw new Exception($"Category '{request.CategoryId}' not found");

        var now = DateTime.Now;

        var newItem = new Item
        {
            Id = request.ItemId,
            Name = request.Name,
            BrandId = brand.Id,
            CategoryId = category.Id,
            IsVariant = false,
            IsVariantOf = null,
            Discontinued = false,
            Description = null,
            WeightUnit = null,
            Weight = null,
            MeasureUnit = null,
            Height = null,
            Length = null,
            Width = null,
            Color = null,
            Color2 = null,
            BulletPoint1 = null,
            BulletPoint2 = null,
            BulletPoint3 = null,
            WarningCode1 = null,
            WarningCode2 = null,
            WarningCode3 = null,
            ChildCouldChokeWarning = false,
            Picture1Url = null,
            Picture2Url = null,
            Picture3Url = null
        };

        _catalogDbContext.Items.Add(newItem);
        await _catalogDbContext.SaveChangesAsync();

        var fetchedEntity = _catalogDbContext.Items
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == request.ItemId);

        if (fetchedEntity is null)
            throw new Exception($"This should not happen - Item ID '{request.ItemId}'");

        await _mediator.Publish(new NewItem{ ItemId = fetchedEntity.Id });
    }
}
