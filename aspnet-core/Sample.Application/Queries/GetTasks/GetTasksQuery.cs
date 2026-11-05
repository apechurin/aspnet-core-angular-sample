using Sample.Shared.Kernel.Application;

namespace Sample.Application.Queries.GetTasks;

public record GetTasksQuery(bool? IsCompleted) : IQuery<GetTasksResult>;
