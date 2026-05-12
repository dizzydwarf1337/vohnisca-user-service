using System.Reflection;
using MediatR;
using Serilog;
using LanguageExt.Common;

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
            var error = Error.New(500, "An unexpected error occurred.");
            var implicitOp = typeof(TResponse).GetMethod(
                "op_Implicit",
                BindingFlags.Static | BindingFlags.Public,
                new[] { typeof(Error) }
            );

            if (implicitOp != null)
                return (TResponse)implicitOp.Invoke(null, new object[] { error })!;
            throw;
        }
    }
}