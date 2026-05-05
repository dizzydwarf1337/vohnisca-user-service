using Application.Commands.User.Users.UpdateUserData;
using Application.Queries.User.Me;
using MediatR;

namespace api.Controllers.User;

public class UserController : BaseController
{
    public UserController(IMediator mediator) : base(mediator) { }
    
    public async Task<object> UpdateUserData(UpdateUserDataCommand command)
    {
        return await HandleRpcResponse(command);
    }
    
    public async Task<object> GetMe()
    {
        var query = new GetMeQuery();
        return await HandleRpcResponse(query);
    } 
}