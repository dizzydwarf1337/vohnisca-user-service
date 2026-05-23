using Application.Commands.User.Friends.DeleteFriend;
using Application.Queries.User.FriendRequests.GetFriends;
using MediatR;

namespace api.Controllers.User;

public class FriendsController : BaseController
{
    public FriendsController(IMediator mediator) : base(mediator)
    {
    }

    public async Task<object> DeleteFriend(DeleteFriendCommand request)
        => await HandleRpcResponse(request);

    public async Task<object> GetFriends(GetFriendsQuery request)
        => await HandleRpcResponse(request);
}