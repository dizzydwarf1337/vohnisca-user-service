using MediatR;
using Microsoft.Extensions.Logging;
using System.Security.Authentication;
using Application.Core.Responses;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;
using Serilog;

namespace Application.Core.Mediatr.Behaviors;

public class ExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, RpcResponse<TResponse>>
    where TRequest : IRequest<RpcResponse<TResponse>>
{
    private readonly ILogger<ExceptionBehavior<TRequest, TResponse>> _logger;

    public ExceptionBehavior(ILogger<ExceptionBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<RpcResponse<TResponse>> Handle(
        TRequest request,
        RequestHandlerDelegate<RpcResponse<TResponse>> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next(cancellationToken);
        }
        catch (Exception ex)
        {
            Log.Error(ex,
                "Unhandled exception occurred during request {RequestName}.",
                typeof(TRequest).Name);

            return ex switch
            {
                UnauthorizedAccessException authEx => HandleAuthorizationError(authEx),
                AuthenticationException authnEx => HandleAuthenticationError(authnEx),
                _ => HandleInternalServerError(ex),
            };
        }
    }

    private RpcResponse<TResponse> HandleAuthorizationError(UnauthorizedAccessException ex)
    {
        Log.Warning(ex,
            "Authorization failed for request {RequestName}. Message: {Message}",
            typeof(TRequest).Name, ex.Message);

        return RpcResponse<TResponse>.Failure("Authorization failed");
    }

    private RpcResponse<TResponse> HandleAuthenticationError(AuthenticationException ex)
    {
        Log.Warning(ex,
            "Authentication failed for request {RequestName}. Message: {Message}",
            typeof(TRequest).Name, ex.Message);

        return RpcResponse<TResponse>.Failure("Authentication failed");
    }

    private RpcResponse<TResponse> HandleInternalServerError(Exception ex)
    {
        Log.Warning(ex,
            "Internal server error in request {RequestName}.",
            typeof(TRequest).Name);

        return RpcResponse<TResponse>.Failure("Internal server error");
    }
}
