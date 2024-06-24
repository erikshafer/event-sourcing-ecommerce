using Legacy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Legacy.Data.DbContexts;

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
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderingDbContext).Assembly);
    }
}

public class OrderingDbContextFactory : IDesignTimeDbContextFactory<OrderingDbContext>
{
    public OrderingDbContext CreateDbContext(params string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrderingDbContext>();

        if (optionsBuilder.IsConfigured)
            return new OrderingDbContext(optionsBuilder.Options);

        optionsBuilder.UseSqlServer("Server=127.0.0.1,1433; Database=LegacyDb; User Id=sa; Password=myStrong_Password123#; Timeout=10; MultipleActiveResultSets=true; TrustServerCertificate=true;");

        return new OrderingDbContext(optionsBuilder.Options);
    }
}
