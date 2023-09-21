using Legacy.Monolith.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Legacy.Monolith.DbContexts;

public class CatalogDbContext : DbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options)
        : base(options)
    {
    }

    public CatalogDbContext()
    {
    }

    public DbSet<Item> Items { get; set; } = default!;

    public DbSet<Brand> Brands { get; set; } = default!;

    public DbSet<Category> Categories { get; set; } = default!;

    public DbSet<Restriction> Restrictions { get; set; } = default!;

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
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

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
    }

    public async Task<List<Item>> GetAllItems(CancellationToken ct = default)
    {
        return await Items.ToListAsync(cancellationToken: ct);
    }

    public async Task<object> GetItemById(int id, CancellationToken ct = default)
    {
        return await Items.FindAsync(id, ct);
    }
}
