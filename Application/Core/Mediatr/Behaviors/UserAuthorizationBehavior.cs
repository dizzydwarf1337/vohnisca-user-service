using Application.Core.Mediatr.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Core.Mediatr.Behaviors;

public class UserAuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : UserRequest<TResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public UserAuthorizationBehavior(IHttpContextAccessor httpContextAccessor) 
        => _httpContextAccessor = httpContextAccessor;
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request.IsSystemRequest)
            return await next(cancellationToken);
        
        var httpContext = _httpContextAccessor.HttpContext;
        var user = httpContext?.User;

        if (user == null || user.Identity is null || !user.Identity.IsAuthenticated || !user.IsInRole("user"))
        {
            throw new UnauthorizedAccessException("Authentication and user role are required.");
        }

        var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            throw new UnauthorizedAccessException("Invalid or missing user identifier claim.");
        }

        var roleClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "user";

        string token = string.Empty;
        if (httpContext!.Request.Headers.TryGetValue("Authorization", out var headerValue))
        {
            var fullToken = headerValue.ToString();
            if (fullToken.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = fullToken.Substring("Bearer ".Length).Trim();
            }
        }

        request.AuthorizeData = new AuthorizeData(userId, roleClaim, token);
        
        return await next(cancellationToken);
    }
}