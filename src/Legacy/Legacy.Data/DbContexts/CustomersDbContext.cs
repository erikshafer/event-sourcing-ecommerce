using Legacy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Legacy.Data.DbContexts;

public class CustomersDbContext : DbContext
{
    public CustomersDbContext(DbContextOptions<CustomersDbContext> options)
        : base(options)
    {
    }

    public CustomersDbContext()
    {
    }

    public DbSet<Customer> Customers { get; set; } = default!;

    public DbSet<Address> Addresses { get; set; } = default!;

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
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomersDbContext).Assembly);
    }
}

public class CustomersDbContextFactory : IDesignTimeDbContextFactory<CustomersDbContext>
{
    public CustomersDbContext CreateDbContext(params string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CustomersDbContext>();

        if (optionsBuilder.IsConfigured)
            return new CustomersDbContext(optionsBuilder.Options);

        optionsBuilder.UseSqlServer("Server=127.0.0.1,1433; Database=LegacyDb; User Id=sa; Password=myStrong_Password123#; Timeout=10; MultipleActiveResultSets=true; TrustServerCertificate=true;");

        return new CustomersDbContext(optionsBuilder.Options);
    }
}
