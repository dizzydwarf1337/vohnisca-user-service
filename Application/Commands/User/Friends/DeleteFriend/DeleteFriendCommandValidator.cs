using FluentValidation;

namespace Application.Commands.User.Friends.DeleteFriend;

public class DeleteFriendCommandValidator : AbstractValidator<DeleteFriendCommand>
{
    public DeleteFriendCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithName("Friend id");
    }
}