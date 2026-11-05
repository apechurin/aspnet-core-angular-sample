using Microsoft.EntityFrameworkCore;
using Sample.Application.Contracts;
using Sample.Application.Ports;
using Sample.Domain.Tasks;
using Sample.Infrastructure.Persistence;

namespace Sample.Infrastructure.Adapters;

public sealed class TaskQueries(TaskDbContext context) : ITaskQueries
{
    public async Task<IReadOnlyList<TaskItemDto>> GetAsync(
        bool? isCompleted,
        CancellationToken cancellationToken = default
    )
    {
        var res = isCompleted.HasValue
            ? await context
                .Tasks.Where(taskItem => taskItem.IsCompleted == isCompleted.Value)
                .OrderBy(taskItem => taskItem.CreatedAt)
                .Select(taskItem => new TaskItemDto(taskItem))
                .ToListAsync(cancellationToken)
            : await context
                .Tasks.OrderBy(taskItem => taskItem.CreatedAt)
                .Select(taskItem => new TaskItemDto(taskItem))
                .ToListAsync(cancellationToken);
        return res;
    }

    public Task<TaskItemDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var taskId = new TaskId(id);
        return context
            .Tasks.Where(taskItem => taskItem.Id == taskId)
            .Select(taskItem => new TaskItemDto(taskItem))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
