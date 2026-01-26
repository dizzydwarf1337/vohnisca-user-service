using Application.Core.Mediatr.Requests;
using LanguageExt;

namespace Application.Commands.User.Notifications.MarkAsUnread;

public class MarkAsUnreadNotificationCommand : UserRequest<Unit>
{
    public Guid NotificationId { get; set; }
}