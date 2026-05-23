
using System.Text.Json.Serialization;

namespace Application.Core.Mediatr.Requests;

public class UserRequest<T> : AuthorizedRequest<T>, IUserRequest;

public interface IUserRequest
{
    [JsonIgnore]
    bool IsSystemRequest { get; }
    
    [JsonIgnore]
    AuthorizeData AuthorizeData { get; set; }
}