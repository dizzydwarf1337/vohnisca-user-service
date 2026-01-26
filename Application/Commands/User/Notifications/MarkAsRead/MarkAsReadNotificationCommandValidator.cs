using FluentValidation;

namespace Application.Commands.User.Notifications.MarkAsRead;

public class MarkAsReadNotificationCommandValidator : AbstractValidator<MarkAsReadNotificationCommand>
{
    public MarkAsReadNotificationCommandValidator()
    {
        RuleFor(x => x.NotificationId).NotEmpty().WithName("Notification id");
    }
}