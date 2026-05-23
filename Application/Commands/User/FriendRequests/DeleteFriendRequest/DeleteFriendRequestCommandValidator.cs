using FluentValidation;

namespace Application.Commands.User.FriendRequests.DeleteFriendRequest;

public class DeleteFriendRequestCommandValidator : AbstractValidator<DeleteFriendRequestCommand>
{
    public DeleteFriendRequestCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithName("Friend request id");
    }
}