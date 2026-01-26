using FluentValidation;

namespace Application.Commands.User.Notifications.MarkAsUnread;

public class MarkAsUnreadNotificationCommandValidator : AbstractValidator<MarkAsUnreadNotificationCommand>
{
    public MarkAsUnreadNotificationCommandValidator()
    {
        RuleFor(x => x.NotificationId).NotEmpty().WithName("Notification id");
    }
}