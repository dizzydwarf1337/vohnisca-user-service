using Application.Core.Extensions;
using Domain.Interfaces.Notifications;
using Domain.Interfaces.Users;
using Domain.Models.Notifications;
using Domain.Models.Notifications.Enums;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Unit = LanguageExt.Unit;

namespace Application.Commands.Admin.Notifications.SendNotification;

public class SendNotificationCommandHandler(
    INotificationRepository notificationRepository,
    IUserRepository userRepository)
    : IRequestHandler<SendNotificationCommand, Either<Error, Unit>>
{

    public async Task<Either<Error, Unit>> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
    {
        return await CheckUserExists(request.UserId, request.Type, cancellationToken)
            .BindAsync(_ => Notification.Create(request.UserId, request.Title, request.Content, request.Type))
            .BindAsync(n => notificationRepository.SaveAsync(n, cancellationToken))
            .MapToUnitAsync();
    }

    private async Task<Either<Error, Unit>> CheckUserExists(Guid? userId, NotificationType type, CancellationToken token)
    {
        if (!userId.HasValue && type == NotificationType.Global)
            return Unit.Default;
        if (!userId.HasValue)
            return Error.New("Invalid user id");
        
        var user = await userRepository.GetByIdAsync(userId.Value, token);
        if (user.IsNone)
            return Error.New("User not found");

        return Unit.Default;
    }
}