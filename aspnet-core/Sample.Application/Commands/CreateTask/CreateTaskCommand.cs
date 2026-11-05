using Sample.Shared.Kernel.Application;

namespace Sample.Application.Commands.CreateTask;

public record CreateTaskCommand(string Title, string? Description) : ICommand<Guid>;
