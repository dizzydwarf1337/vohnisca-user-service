namespace api.Core.Extensions;

public static class CorsPolicies
{
    public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(opt =>
        {
            opt.AddPolicy("CorsPolicy",
                policy => { policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); });
        });

        return services;
    }
}