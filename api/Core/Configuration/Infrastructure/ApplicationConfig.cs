namespace api.Core.Configuration.Infrastructure;

public static class ApplicationConfig
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        return services;
    }
}