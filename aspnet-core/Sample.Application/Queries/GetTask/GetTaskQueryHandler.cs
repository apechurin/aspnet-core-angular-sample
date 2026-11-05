using Sample.Application.Contracts;
using Sample.Application.Ports;
using Sample.Shared.Kernel;
using Sample.Shared.Kernel.Application;

namespace Sample.Application.Queries.GetTask;

public class GetTaskQueryHandler(ITaskQueries queries) : IQueryHandler<GetTaskQuery, TaskItemDto>
{
    public async Task<TaskItemDto> Handle(GetTaskQuery query, CancellationToken cancellationToken)
    {
        var dto = await queries.GetByIdAsync(query.Id, cancellationToken);
        if (dto is null)
            throw new NotFoundException(ApplicationErrors.TaskNotFound, "Task is not found");

        return dto;
    }
}
