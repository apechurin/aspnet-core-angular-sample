using MediatR;
using Sample.Application.Ports;
using Sample.Domain.Tasks;
using Sample.Shared.Kernel.Application;

namespace Sample.Application.Commands.DeleteTask;

public class DeleteTaskCommandHandler(ITaskRepository repository, IUnitOfWork uow)
    : ICommandHandler<DeleteTaskCommand, Unit>
{
    public async Task<Unit> Handle(DeleteTaskCommand command, CancellationToken cancellationToken)
    {
        await repository.RemoveAsync(new TaskId(command.Id), cancellationToken);
        await uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
