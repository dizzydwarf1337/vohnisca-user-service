using Application.Commands.User.FriendRequests.AcceptFriendRequest;
using Application.Commands.User.FriendRequests.RejectFriendRequest;
using Application.Commands.User.FriendRequests.SendFriendRequest;
using Application.Queries.User.FriendRequests.GetFriendRequests;
using MediatR;
namespace api.Controllers.User;

public class FriendRequestController : BaseController
{
    public FriendRequestController(IMediator mediator) : base(mediator) { }
        
    public async Task<object> Send(SendFriendRequestCommand command)
        => await HandleRpcResponse(command);
    

    public async Task<object> Accept(AcceptFriendRequestCommand command)
        => await HandleRpcResponse(command);
    

    public async Task<object> Reject(RejectFriendRequestCommand command)
        => await HandleRpcResponse(command);

    public async Task<object> ReceivedRequests()
        => await HandleRpcResponse(new GetFriendRequestsQuery());
}