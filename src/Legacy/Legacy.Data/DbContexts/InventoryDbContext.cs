using Legacy.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Legacy.Data.DbContexts;

public class InventoryDbContext : DbContext
{
    public InventoryDbContext(DbContextOptions<OrderingDbContext> options)
        : base(options)
    {
    }

    public InventoryDbContext()
    {
    }

    public DbSet<Inventory> Inventories { get; set; } = default!;

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
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
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderingDbContext).Assembly);
    }

    public async Task<List<Inventory>> GetAllInventories(CancellationToken ct = default)
    {
        return await Inventories.ToListAsync(ct);
    }

    public async Task<Inventory> GetInventoryById(int id, CancellationToken ct = default)
    {
        return await Inventories.FindAsync(id, ct);
    }
}
