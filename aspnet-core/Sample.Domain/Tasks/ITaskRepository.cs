namespace Sample.Domain.Tasks;

public interface ITaskRepository
{
    Task<TaskItem?> GetByIdAsync(TaskId id, CancellationToken cancellationToken = default);

    Task AddAsync(TaskItem taskItem, CancellationToken cancellationToken = default);

    Task RemoveAsync(TaskId id, CancellationToken cancellationToken = default);

    void SetVersion(TaskItem taskItem, uint version);
}
