using Legacy.Monolith.Core.Entities.Models;
using Legacy.Monolith.Orders.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Legacy.Monolith.Orders.DbContexts;

public class OrderingDbContext : DbContext
{
    public OrderingDbContext(DbContextOptions<OrderingDbContext> options)
        : base(options)
    {
    }

    public OrderingDbContext()
    {
    }

    public DbSet<Order> Orders { get; set; } = default!;

    public DbSet<Payment> Payments { get; set; } = default!;

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
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderingDbContext).Assembly);
    }

    public async Task<List<Order>> GetAllOrders(CancellationToken ct = default)
    {
        return await Orders.ToListAsync(ct);
    }

    public async Task<Order> GetOrderById(int id, CancellationToken ct = default)
    {
        return await Orders.FindAsync(id, ct);
    }
}
