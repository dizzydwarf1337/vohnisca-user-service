using FluentValidation;

namespace Application.Commands.User.FriendRequests.SendFriendRequest;

public class SendFriendRequestCommandValidator : AbstractValidator<SendFriendRequestCommand>
{
    public SendFriendRequestCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithName("Username");
    }
}