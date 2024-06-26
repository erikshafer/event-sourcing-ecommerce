using Legacy.Data.Seeds;
using Legacy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Legacy.Data.DbContexts;

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
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);

        modelBuilder.Entity<Brand>()
            .HasData(CatalogSeeder.GenerateBrands());

        modelBuilder.Entity<Category>()
            .HasData(CatalogSeeder.GenerateCategories());

        modelBuilder.Entity<Item>()
            .HasData(CatalogSeeder.GenerateItems());

        modelBuilder.Entity<Restriction>()
            .HasData(CatalogSeeder.GenerateRestrictions());
    }
}

public class CatalogDbContextFactory : IDesignTimeDbContextFactory<CatalogDbContext>
{
    public CatalogDbContext CreateDbContext(params string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CatalogDbContext>();

        if (optionsBuilder.IsConfigured)
            return new CatalogDbContext(optionsBuilder.Options);

        optionsBuilder.UseSqlServer("Server=127.0.0.1,1433; Database=LegacyDb; User Id=sa; Password=myStrong_Password123#; Timeout=10; MultipleActiveResultSets=true; TrustServerCertificate=true;");

        return new CatalogDbContext(optionsBuilder.Options);
    }
}
