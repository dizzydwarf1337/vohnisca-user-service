using Application.Core.Mediatr.Requests;
using LanguageExt;

namespace Application.Commands.User.FriendRequests.DeleteFriendRequest;

public class DeleteFriendRequestCommand : UserRequest<Unit>
{
    public Guid Id { get; set; }
}