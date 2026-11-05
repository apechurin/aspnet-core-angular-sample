using Microsoft.EntityFrameworkCore;
using Sample.Application.Ports;
using Sample.Infrastructure.Persistence;
using Sample.Shared.Kernel;

namespace Sample.Infrastructure.Adapters;

public sealed class TaskUnitOfWork(TaskDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException e)
        {
            throw new ConflictException(
                InfrastructureErrors.TaskUpdateConflict,
                "Failed to save task changes due to a conflict",
                e
            );
        }
    }
}
