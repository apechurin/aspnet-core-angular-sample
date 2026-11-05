using Sample.Application.Ports;
using Sample.Shared.Kernel.Application;

namespace Sample.Application.Queries.GetTasks;

public class GetTasksQueryHandler(ITaskQueries queries)
    : IQueryHandler<GetTasksQuery, GetTasksResult>
{
    public async Task<GetTasksResult> Handle(
        GetTasksQuery query,
        CancellationToken cancellationToken
    )
    {
        var dtos = await queries.GetAsync(query.IsCompleted, cancellationToken);

        return new GetTasksResult(dtos);
    }
}
