using Application.Core.Mediatr.Requests;
using Domain.Models.Notifications.Enums;
using LanguageExt;

namespace Application.Commands.Admin.Notifications.SendNotification;

public class SendNotificationCommand : AdminRequest<Unit>
{
    public Guid? UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public NotificationType Type { get; set; }
}