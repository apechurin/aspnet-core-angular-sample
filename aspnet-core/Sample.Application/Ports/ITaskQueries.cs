using Sample.Application.Contracts;

namespace Sample.Application.Ports;

public interface ITaskQueries
{
    Task<IReadOnlyList<TaskItemDto>> GetAsync(
        bool? isCompleted,
        CancellationToken cancellationToken = default
    );

    Task<TaskItemDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
