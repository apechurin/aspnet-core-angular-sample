using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Sample.Api.Contracts;
using Sample.Application.Commands.CreateTask;
using Sample.Application.Commands.DeleteTask;
using Sample.Application.Commands.UpdateTask;
using Sample.Application.Contracts;
using Sample.Application.Queries.GetTask;
using Sample.Application.Queries.GetTasks;

namespace Sample.Api.Endpoints;

public static class TaskEndpoints
{
    public static IEndpointRouteBuilder MapTaskEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/tasks").WithTags("Task");

        group
            .MapGet(
                "/",
                async (bool? isCompleted, ISender sender, CancellationToken cancellationToken) =>
                {
                    var res = await sender.Send(new GetTasksQuery(isCompleted), cancellationToken);

                    return new GetTasksResponse(res.Items, res.Items.Count);
                }
            )
            .WithName("GetTasks")
            .WithSummary("Get tasks.")
            .ProducesValidationProblem()
            .Produces<GetTasksResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError);

        group
            .MapGet(
                "/{id}",
                async (Guid id, ISender sender, CancellationToken cancellationToken) =>
                {
                    var res = await sender.Send(new GetTaskQuery(id), cancellationToken);

                    return res;
                }
            )
            .WithName("GetTask")
            .WithSummary("Get a task.")
            .Produces<TaskItemDto>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError);

        group
            .MapPost(
                "/",
                async (
                    CreateTaskRequest request,
                    ISender sender,
                    CancellationToken cancellationToken
                ) =>
                {
                    var res = await sender.Send(
                        new CreateTaskCommand(
                            Title: request.Title,
                            Description: request.Description
                        ),
                        cancellationToken
                    );

                    return new CreateTaskResponse(res);
                }
            )
            .WithName("CreateTask")
            .WithSummary("Create a new task.")
            .Produces<CreateTaskResponse>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status500InternalServerError);

        group
            .MapPut(
                "/{id}",
                async (
                    Guid id,
                    UpdateTaskRequest request,
                    ISender sender,
                    CancellationToken cancellationToken
                ) =>
                {
                    var res = await sender.Send(
                        new UpdateTaskCommand(
                            Id: id,
                            Title: request.Title,
                            Description: request.Description,
                            IsCompleted: request.IsCompleted,
                            Version: request.Version
                        ),
                        cancellationToken
                    );

                    return Results.NoContent();
                }
            )
            .WithName("UpdateTask")
            .WithSummary("Update the task.")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status409Conflict)
            .ProducesProblem(StatusCodes.Status500InternalServerError);

        group
            .MapDelete(
                "/{id}",
                async (Guid id, ISender sender, CancellationToken cancellationToken) =>
                {
                    var res = await sender.Send(new DeleteTaskCommand(id), cancellationToken);

                    return Results.NoContent();
                }
            )
            .WithName("DeleteTask")
            .WithSummary("Delete the task.")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status500InternalServerError);

        return app;
    }
}
