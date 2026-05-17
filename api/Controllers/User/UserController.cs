using Application.Commands.User.Users.UpdateUserData;
using Application.Queries.User.Me;
using MediatR;

namespace api.Controllers.User;

public class UserController : BaseController
{
    public UserController(IMediator mediator) : base(mediator)
    {
    }

    public async Task<object> UpdateUserData(UpdateUserDataCommand request)
    {
        return await HandleRpcResponse(request);
    }

    public async Task<object> GetMe()
    {
        return await HandleRpcResponse(new GetMeQuery());
    }
}