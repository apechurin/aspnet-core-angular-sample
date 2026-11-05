using MediatR;
using Sample.Shared.Kernel.Application;

namespace Sample.Application.Commands.DeleteTask;

public record DeleteTaskCommand(Guid Id) : ICommand<Unit>;
