using Application.Core.Mediatr.Requests;
using LanguageExt;

namespace Application.Commands.User.FriendRequests.SendFriendRequest;

public class SendFriendRequestCommand : UserRequest<Unit>
{
    public string UserName { get; set; }
}