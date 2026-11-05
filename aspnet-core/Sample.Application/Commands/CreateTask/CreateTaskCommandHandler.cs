using Sample.Application.Ports;
using Sample.Domain.Tasks;
using Sample.Shared.Kernel.Application;

namespace Sample.Application.Commands.CreateTask;

public class CreateTaskCommandHandler(ITaskRepository repository, IUnitOfWork uow)
    : ICommandHandler<CreateTaskCommand, Guid>
{
    public async Task<Guid> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
    {
        var taskItem = TaskItem.Create(command.Title, command.Description);

        await repository.AddAsync(taskItem, cancellationToken);
        await uow.SaveChangesAsync(cancellationToken);

        return taskItem.Id.Value;
    }
}
