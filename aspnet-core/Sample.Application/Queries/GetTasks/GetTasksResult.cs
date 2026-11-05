using Sample.Application.Contracts;

namespace Sample.Application.Queries.GetTasks;

public record GetTasksResult(IReadOnlyList<TaskItemDto> Items);
