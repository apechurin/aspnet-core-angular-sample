using MediatR;
using Sample.Shared.Kernel.Application;

namespace Sample.Application.Commands.UpdateTask;

public record UpdateTaskCommand(
    Guid Id,
    string Title,
    string? Description,
    bool IsCompleted,
    uint Version
) : ICommand<Unit>;
