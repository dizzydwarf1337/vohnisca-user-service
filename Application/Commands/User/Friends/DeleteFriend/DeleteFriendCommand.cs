using Application.Core.Mediatr.Requests;
using LanguageExt;

namespace Application.Commands.User.Friends.DeleteFriend;

public class DeleteFriendCommand : UserRequest<Unit>
{
    public Guid Id { get; set; }
}