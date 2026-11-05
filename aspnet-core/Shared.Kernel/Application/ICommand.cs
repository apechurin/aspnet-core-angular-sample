using MediatR;

namespace Sample.Shared.Kernel.Application;

public interface ICommand<TResult> : IRequest<TResult> { }
