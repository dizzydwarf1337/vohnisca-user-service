using Application.Core.Responses;
using EdjCase.JsonRpc.Router;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace api.Controllers;

[RpcRoute("")]
public class BaseController : RpcController
{
    private readonly IMediator  _mediator;
    
    public BaseController(IMediator mediator)
        => _mediator = mediator;

    public async Task<object> HandleRpcResponse<T>(IRequest<Either<Error,T>> command)
    {
        var result = await _mediator.Send(command);
        return result.Match(
            Right: RpcResponse<T>.Success,
            Left: error => RpcResponse<T>.Failure(error.Message)
        );
    } 
}