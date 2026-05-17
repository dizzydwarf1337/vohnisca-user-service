using Application.Commands.User.FriendRequests.AcceptFriendRequest;
using Application.Commands.User.FriendRequests.RejectFriendRequest;
using Application.Commands.User.FriendRequests.SendFriendRequest;
using Application.Queries.User.FriendRequests.GetFriendRequests;
using MediatR;

namespace api.Controllers.User;

public class FriendRequestController : BaseController
{
    public FriendRequestController(IMediator mediator) : base(mediator)
    {
    }

    public async Task<object> SendFriendRequest(SendFriendRequestCommand request)
        => await HandleRpcResponse(request);


    public async Task<object> AcceptFriendRequest(AcceptFriendRequestCommand request)
        => await HandleRpcResponse(request);


    public async Task<object> RejectFriendRequest(RejectFriendRequestCommand request)
        => await HandleRpcResponse(request);

    public async Task<object> ReceivedFriendRequests()
        => await HandleRpcResponse(new GetFriendRequestsQuery());
}