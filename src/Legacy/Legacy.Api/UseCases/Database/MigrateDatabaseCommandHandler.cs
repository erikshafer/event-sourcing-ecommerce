using Legacy.Data.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Legacy.Api.UseCases.Database;

public class MigrateDatabaseCommandHandler : IRequestHandler<MigrateDatabaseCommand, MigrateDataResponse>
{
    private readonly CatalogDbContext _catalogDbContext;
    private readonly InventoryDbContext _inventoryDbContext;
    private readonly ListingDbContext _listingDbContext;
    private readonly OrderingDbContext _orderingDbContext;

    public MigrateDatabaseCommandHandler(
        CatalogDbContext catalogDbContext,
        InventoryDbContext inventoryDbContext,
        ListingDbContext listingDbContext,
        OrderingDbContext orderingDbContext)
    {
        _catalogDbContext = catalogDbContext;
        _inventoryDbContext = inventoryDbContext;
        _listingDbContext = listingDbContext;
        _orderingDbContext = orderingDbContext;
    }

    public async Task<MigrateDataResponse> Handle(MigrateDatabaseCommand command, CancellationToken ct)
    {
        await _catalogDbContext.Database.MigrateAsync(ct);
        await _inventoryDbContext.Database.MigrateAsync(ct);
        await _listingDbContext.Database.MigrateAsync(ct);
        await _orderingDbContext.Database.MigrateAsync(ct);

        return new MigrateDataResponse();
    }
}
