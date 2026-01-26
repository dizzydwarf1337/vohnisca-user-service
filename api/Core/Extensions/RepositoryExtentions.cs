using Domain.Interfaces.Chats;
using Domain.Interfaces.Messages;
using Domain.Interfaces.Notifications;
using Domain.Interfaces.Users;
using Persistence.Repositories.Chats;
using Persistence.Repositories.Messages;
using Persistence.Repositories.Notifications;
using Persistence.Repositories.Users;

namespace api.Core.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<IMessageRepository, MessagesRepository>();
        services.AddScoped<IMessageAttachmentRepository, MessageAttachmentRepository>();
        services.AddScoped<IMessageReadStatusRepository, MessageReadStatusRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();

        return services;
    }
}