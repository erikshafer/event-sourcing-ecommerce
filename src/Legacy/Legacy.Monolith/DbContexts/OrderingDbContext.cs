using Legacy.Monolith.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Legacy.Monolith.DbContexts;

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
}
