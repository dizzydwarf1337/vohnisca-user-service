using Amazon.Runtime;
using Amazon.S3;
using Application.Commands.User.Users.CreateUserData;
using Application.Consumers.Users.UserCreated;
using Application.Core.Mediatr.Behaviors;
using Application.Interfaces.Storage;
using FluentValidation;
using MassTransit;
using MediatR;
using Persistence.Storage;

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
                cfg.ReceiveEndpoint("user-service-user-created",
                    e => { e.ConfigureConsumer<UserCreatedConsumer>(context); });
                cfg.ConfigureEndpoints(context);
            });
        });
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(CreateUserDataCommand).Assembly); });

        services.AddSingleton<R2StorageConfig>(_ => new R2StorageConfig
        {
            AccountId = Environment.GetEnvironmentVariable("R2_ACCOUNT_ID")!,
            AccessKeyId = Environment.GetEnvironmentVariable("R2_ACCESS_KEY_ID")!,
            SecretAccessKey = Environment.GetEnvironmentVariable("R2_SECRET_ACCESS_KEY")!,
            BucketName = Environment.GetEnvironmentVariable("R2_BUCKET_NAME")!,
            PublicBaseUrl = Environment.GetEnvironmentVariable("R2_PUBLIC_BASE_URL")!,
        });

        services.AddSingleton<IAmazonS3>(sp =>
        {
            var cfg = sp.GetRequiredService<R2StorageConfig>();
            return new AmazonS3Client(
                cfg.AccessKeyId,
                cfg.SecretAccessKey,
                new AmazonS3Config
                {
                    ServiceURL = cfg.Endpoint,
                    ForcePathStyle = true,
                    RequestChecksumCalculation = RequestChecksumCalculation.WHEN_REQUIRED,
                    ResponseChecksumValidation = ResponseChecksumValidation.WHEN_REQUIRED,
                }
            );
        });

        services.AddScoped<IBlobStorage, CloudflareR2BlobStorage>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UserAuthorizationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AdminAuthorizationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(typeof(CreateUserDataCommandValidator).Assembly);

        services.AddLogging();
        return services;
    }
}