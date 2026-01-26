using System.Security.Claims;
using Application.Core.Mediatr.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Core.Mediatr.Behaviors;

public class AdminAuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : AdminRequest<TResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public AdminAuthorizationBehavior(IHttpContextAccessor httpContextAccessor) 
        => _httpContextAccessor = httpContextAccessor;
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request.IsSystemRequest)
            return await next(cancellationToken);
        
        var httpContext = _httpContextAccessor.HttpContext;
        var user = httpContext?.User;
        
        if (user == null || !user.IsInRole("admin"))
        {
            throw new UnauthorizedAccessException("Only administrators are allowed to perform this action."); 
        }

        var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            throw new UnauthorizedAccessException("Invalid or missing user identifier claim.");
        }

        var token = string.Empty;
        if (httpContext!.Request.Headers.TryGetValue("Authorization", out var headerValue))
        {
            var fullToken = headerValue.ToString();
            if (fullToken.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = fullToken.Substring("Bearer ".Length).Trim();
            }
        }

        request.AuthorizeData = new AuthorizeData(userId, "admin", token);
        
        return await next(cancellationToken);
    }
}