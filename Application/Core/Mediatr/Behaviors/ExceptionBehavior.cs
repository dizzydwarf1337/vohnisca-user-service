using MediatR;
using Serilog;

namespace Application.Core.Mediatr.Behaviors;

public class ExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next(cancellationToken);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Unhandled exception occurred during request {RequestName}.", typeof(TRequest).Name);
            throw;
        }
    }
}