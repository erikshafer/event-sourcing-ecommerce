using Legacy.Data.Seeds;
using Legacy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Legacy.Data.DbContexts;

public class InventoryDbContext : DbContext
{
    public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
        : base(options)
    {
    }

    public InventoryDbContext()
    {
    }

    public DbSet<Inventory> Inventories { get; set; } = default!;
    public DbSet<InventoryHistory> InventoryHistories { get; set; } = default!;
    public DbSet<Warehouse> Warehouses { get; set; } = default!;

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
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryDbContext).Assembly);

        modelBuilder.Entity<Warehouse>()
            .HasData(InventorySeeder.GenerateWarehouses());

        modelBuilder.Entity<Inventory>()
            .HasData(InventorySeeder.GenerateInventories());

        modelBuilder.Entity<InventoryHistory>()
            .HasData(InventorySeeder.GenerateInventoryHistories());
    }
}

public class InventoryDbContextFactory : IDesignTimeDbContextFactory<InventoryDbContext>
{
    public InventoryDbContext CreateDbContext(params string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<InventoryDbContext>();

        if (optionsBuilder.IsConfigured)
            return new InventoryDbContext(optionsBuilder.Options);

        optionsBuilder.UseSqlServer("Server=127.0.0.1,1433; Database=LegacyDb; User Id=sa; Password=myStrong_Password123#; Timeout=10; MultipleActiveResultSets=true; TrustServerCertificate=true;");

        return new InventoryDbContext(optionsBuilder.Options);
    }
}
