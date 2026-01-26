using Application.Commands.User.Users.UpdateUserData;
using EdjCase.JsonRpc.Router;
using MediatR;

namespace api.Controllers.User;

public class UserController : BaseController
{
    public UserController(IMediator mediator) : base(mediator) { }
    
    [RpcRoute("user/update-user-data")]
    public async Task<object> UserUpdateUserData(UpdateUserDataCommand command)
    {
        return await HandleRpcResponse(command);
    }
}