using MediatR;

namespace Application.Models;

public abstract class SyncRequestHandler<TRequest, TResponse>
    : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            throw new TaskCanceledException();

        return Task.FromResult(Handle(request));
    }

    protected abstract TResponse Handle(TRequest request);
}