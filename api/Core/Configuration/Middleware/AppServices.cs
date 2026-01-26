using EdjCase.JsonRpc.Router;

namespace api.Core.Configuration.Middleware;

public static class AppServices
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddJsonRpc(new RpcServerConfiguration()
        {
            ShowServerExceptions = true,
        });
        return services;
    }
}