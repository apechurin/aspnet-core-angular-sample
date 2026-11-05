using Microsoft.EntityFrameworkCore;
using Sample.Domain.Tasks;

namespace Sample.Infrastructure.Persistence;

public sealed class TaskDbContext : DbContext
{
    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    public TaskDbContext(DbContextOptions<TaskDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("sample");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskDbContext).Assembly);
    }
}
