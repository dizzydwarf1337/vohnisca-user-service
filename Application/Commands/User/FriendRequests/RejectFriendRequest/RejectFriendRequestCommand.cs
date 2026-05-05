using Application.Core.Mediatr.Requests;
using LanguageExt;

namespace Application.Commands.User.FriendRequests.RejectFriendRequest;

public class RejectFriendRequestCommand : UserRequest<Unit>
{
    public Guid Id { get; set; }
}