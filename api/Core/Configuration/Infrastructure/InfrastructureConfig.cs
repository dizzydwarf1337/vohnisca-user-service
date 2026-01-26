using Application.Commands.User.Users.CreateUserData;
using Application.Consumers.Users.UserCreated;
using Application.Core.Mediatr.Behaviors;
using FluentValidation;
using MassTransit;
using MediatR;

namespace api.Core.Configuration.Infrastructure;

public static class InfrastructureConfig
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<UserCreatedConsumer>();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.UseRawJsonDeserializer();
                cfg.Message<UserCreatedEvent>(m => m.SetEntityName("user-created"));
                cfg.Host("rabbitmq", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ReceiveEndpoint("user-service-user-created", e =>
                {
                    e.ConfigureConsumer<UserCreatedConsumer>(context);
                });
                cfg.ConfigureEndpoints(context);
            });
        });
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CreateUserDataCommand).Assembly);
        });

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AdminAuthorizationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UserAuthorizationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionBehavior<,>));

        services.AddValidatorsFromAssembly(typeof(CreateUserDataCommandValidator).Assembly);
        
        services.AddLogging();
        return services;
    }
}