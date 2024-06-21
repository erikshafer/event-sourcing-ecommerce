using Legacy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Legacy.Data.DbContexts;

public class ListingDbContext : DbContext
{
    public ListingDbContext(DbContextOptions<ListingDbContext> options)
        : base(options)
    {
    }

    public ListingDbContext()
    {
    }

    public DbSet<Listing> Listings { get; set; } = default!;

    public DbSet<ListingError> ListingErrors { get; set; } = default!;

    public override Task<int> SaveChangesAsync(CancellationToken ct = new())
    {
        var now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedOn = now;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedOn = now;
                    break;
            }
        }

        return base.SaveChangesAsync(ct);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ListingDbContext).Assembly);
    }

    public async Task<List<Listing>> GetAllListings(CancellationToken ct = default)
    {
        return await Listings.ToListAsync(cancellationToken: ct);
    }

    public async Task<List<Listing>> GetAllActiveListings(CancellationToken ct = default)
    {
        return await Listings.Where(x => x.IsActive).ToListAsync(cancellationToken: ct);
    }

    public async Task<List<ListingError>> GetAllListingErrors(CancellationToken ct = default)
    {
        return await ListingErrors.ToListAsync(cancellationToken: ct);
    }

    public async Task<List<ListingError>> GetListingErrorsByItemId(int itemId, CancellationToken ct = default)
    {
        return await ListingErrors.Where(x => x.ItemId == itemId).ToListAsync(cancellationToken: ct);
    }

    public async Task<List<ListingError>> GetListingErrorsByMarketplaceId(int marketplaceId, CancellationToken ct = default)
    {
        return await ListingErrors.Where(x => x.MarketplaceId == marketplaceId).ToListAsync(cancellationToken: ct);
    }
}

public class ListingDbContextFactory : IDesignTimeDbContextFactory<ListingDbContext>
{
    public ListingDbContext CreateDbContext(params string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ListingDbContext>();

        if (optionsBuilder.IsConfigured)
            return new ListingDbContext(optionsBuilder.Options);

        optionsBuilder.UseSqlServer("Server=127.0.0.1,1433; Database=LegacyDb; User Id=sa; Password=myStrong_Password123#; Timeout=10; MultipleActiveResultSets=true; TrustServerCertificate=true;");

        return new ListingDbContext(optionsBuilder.Options);
    }
}
