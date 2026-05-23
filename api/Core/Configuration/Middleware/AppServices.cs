using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using EdjCase.JsonRpc.Router;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace api.Core.Configuration.Middleware;

public static class AppServices
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddHttpContextAccessor();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });
        services.AddJsonRpc(new RpcServerConfiguration()
        {
            ShowServerExceptions = false,
            JsonSerializerSettings = new JsonSerializerOptions
            {
                WriteIndented = true,
                MaxDepth = 10,
                PropertyNameCaseInsensitive = true,
                Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase),
                },
            },
                
        });
        return services;
    }
}