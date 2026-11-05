using Sample.Application.Contracts;
using Sample.Shared.Kernel.Application;

namespace Sample.Application.Queries.GetTask;

public record GetTaskQuery(Guid Id) : IQuery<TaskItemDto>;
