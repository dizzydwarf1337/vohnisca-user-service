namespace Application.Core.Mediatr.Requests;

public class UserRequest<T> : AuthorizedRequest<T>, IUserRequest;

public interface IUserRequest
{
    bool IsSystemRequest { get; }
    AuthorizeData AuthorizeData { get; set; }
}