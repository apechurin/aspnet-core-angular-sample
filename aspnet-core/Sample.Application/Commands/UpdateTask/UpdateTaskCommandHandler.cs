using MediatR;
using Sample.Application.Ports;
using Sample.Domain.Tasks;
using Sample.Shared.Kernel;
using Sample.Shared.Kernel.Application;

namespace Sample.Application.Commands.UpdateTask;

public class UpdateTaskCommandHandler(ITaskRepository repository, IUnitOfWork uow)
    : ICommandHandler<UpdateTaskCommand, Unit>
{
    public async Task<Unit> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
    {
        var taskItem = await repository.GetByIdAsync(new TaskId(command.Id), cancellationToken);
        if (taskItem is null)
            throw new NotFoundException(ApplicationErrors.TaskNotFound, "Task is not found");

        repository.SetVersion(taskItem, command.Version);

        taskItem.Change(command.Title, command.Description, command.IsCompleted);
        await uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
