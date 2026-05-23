using FluentValidation;

namespace Application.Commands.User.FriendRequests.CancelFriendRequest;

public class CancelFriendRequestCommandValidator : AbstractValidator<CancelFriendRequestCommand>
{
    public CancelFriendRequestCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithName("Friend request id");
    }
}