using MediatR;

namespace Sample.Shared.Kernel.Application;

public interface IQuery<TResult> : IRequest<TResult> { }
