using Application.Core.Mediatr.Requests;
using LanguageExt;

namespace Application.Commands.User.Notifications.MarkAsRead;

public class MarkAsReadNotificationCommand : UserRequest<Unit>
{
    public Guid NotificationId { get; set; }
}