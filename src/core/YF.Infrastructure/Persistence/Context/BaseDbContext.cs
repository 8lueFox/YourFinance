using Microsoft.EntityFrameworkCore;

namespace YF.Infrastructure.Persistence.Context;

public abstract class BaseDbContext : DbContext
{
    protected BaseDbContext(DbContextOptions opts)
        :base(opts)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }

    public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        int result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }
}
