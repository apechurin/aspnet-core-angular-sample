using MediatR;

namespace Sample.Shared.Kernel.Application;

public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
    where TQuery : IQuery<TResult> { }
