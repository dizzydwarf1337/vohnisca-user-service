using Domain.Models.Notifications.Enums;
using FluentValidation;

namespace Application.Commands.Admin.Notifications.SendNotification;

public class SendNotificationCommandValidator : AbstractValidator<SendNotificationCommand>
{
    public SendNotificationCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().When(x => x.Type == NotificationType.Individual);
        RuleFor(x => x.UserId).Empty().When(x => x.Type == NotificationType.Global);
        RuleFor(x => x.Title).NotEmpty().Length(2, 50);
        RuleFor(x => x.Content).NotEmpty().Length(5, 200);
    }
}