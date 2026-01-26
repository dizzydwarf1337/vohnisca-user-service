using Application.Core.Extensions;
using Domain.Interfaces.Notifications;
using Domain.Models.Notifications;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Unit = LanguageExt.Unit;

namespace Application.Commands.User.Notifications.MarkAsRead;

public class MarkAsReadNotificationCommandHandler : IRequestHandler<MarkAsReadNotificationCommand, Either<Error, Unit>>
{
    private readonly INotificationRepository _notificationRepository;
    
    public MarkAsReadNotificationCommandHandler(INotificationRepository notificationRepository)
        => _notificationRepository = notificationRepository;
    
    public async Task<Either<Error, Unit>> Handle(MarkAsReadNotificationCommand request, CancellationToken cancellationToken)
    {
        return await GetNotification(request.NotificationId, cancellationToken)
            .BindAsync(n => CheckAccess(n, request.AuthorizeData!.UserId))
            .BindAsync(n => n.MarkAsRead())
            .BindAsync(n => _notificationRepository.UpdateAsync(n, cancellationToken))
            .MapToUnitAsync();
    }

    private async Task<Either<Error, Notification>> GetNotification(Guid notificationId, CancellationToken token)
    {
        var notification = await _notificationRepository.GetByIdAsync(notificationId, token);
        return notification.Match<Either<Error, Notification>>(
            Some: n => n,
            None: Error.New("Notification not found")
        );
    }

    private static Either<Error, Notification> CheckAccess(Notification notification, Guid userId)
    {
        return notification.UserId != userId
            ? Error.New("Can read only your notifications")
            : notification;
    }
}