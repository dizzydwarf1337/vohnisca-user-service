using Application.Core.Mediatr.Requests;
using LanguageExt;

namespace Application.Commands.User.FriendRequests.AcceptFriendRequest;

public class AcceptFriendRequestCommand : UserRequest<Unit>
{
    public Guid Id { get; set; }
}