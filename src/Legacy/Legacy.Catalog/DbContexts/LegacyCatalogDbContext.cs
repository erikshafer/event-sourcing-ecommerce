using Legacy.Catalog.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Legacy.Catalog.DbContexts;

public class LegacyCatalogDbContext : DbContext
{
    public LegacyCatalogDbContext(DbContextOptions<LegacyCatalogDbContext> options)
        : base(options)
    {
    }

    public LegacyCatalogDbContext()
    {
    }

    public DbSet<Item> Items { get; set; } = default!;

    public DbSet<Brand> Brands { get; set; } = default!;

    public DbSet<Category> Categories { get; set; } = default!;

    public DbSet<Restriction> Restrictions { get; set; } = default!;

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var now = DateTime.UtcNow; // pref. injecting a datetime machine

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
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LegacyCatalogDbContext).Assembly);
    }
}
