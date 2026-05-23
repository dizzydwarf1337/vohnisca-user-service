using Application.Core.Mediatr.Requests;
using LanguageExt;

namespace Application.Commands.User.FriendRequests.CancelFriendRequest;

public class CancelFriendRequestCommand : UserRequest<Unit>
{
    public Guid Id { get; set; }
}