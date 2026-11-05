using Sample.Application.Contracts;

namespace Sample.Api.Contracts;

public record GetTasksResponse(IReadOnlyList<TaskItemDto> Items, int TotalCount);
