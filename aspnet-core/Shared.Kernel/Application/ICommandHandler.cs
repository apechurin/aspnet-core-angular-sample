using MediatR;

namespace Sample.Shared.Kernel.Application;

public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
    where TCommand : ICommand<TResult> { }
