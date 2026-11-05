using Microsoft.EntityFrameworkCore;
using Sample.Domain.Tasks;
using Sample.Shared.Kernel;

namespace Sample.Infrastructure.Persistence.Repositories;

public sealed class TaskRepository(TaskDbContext context) : ITaskRepository
{
    public Task<TaskItem?> GetByIdAsync(TaskId id, CancellationToken cancellationToken = default) =>
        context.Tasks.FirstOrDefaultAsync(taskItem => taskItem.Id == id, cancellationToken);

    public async Task AddAsync(TaskItem taskItem, CancellationToken cancellationToken = default)
    {
        await context.AddAsync(taskItem, cancellationToken);
    }

    public async Task RemoveAsync(TaskId id, CancellationToken cancellationToken = default)
    {
        var taskItem = await context.Tasks.FindAsync(id);
        if (taskItem is null)
            throw new NotFoundException(InfrastructureErrors.TaskNotFound, "Task is not found");

        context.Tasks.Remove(taskItem);
    }

    public void SetVersion(TaskItem taskItem, uint version)
    {
        context.Entry(taskItem).Property(t => t.Version).OriginalValue = version;
    }
}
