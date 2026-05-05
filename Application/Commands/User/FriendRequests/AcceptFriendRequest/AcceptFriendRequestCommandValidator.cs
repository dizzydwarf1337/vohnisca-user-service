using FluentValidation;

namespace Application.Commands.User.FriendRequests.AcceptFriendRequest;

public class AcceptFriendRequestCommandValidator : AbstractValidator<AcceptFriendRequestCommand>
{
    public AcceptFriendRequestCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithName("Friend request id");
    }
}