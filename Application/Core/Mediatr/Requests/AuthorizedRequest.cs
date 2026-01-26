using Newtonsoft.Json;

namespace Application.Core.Mediatr.Requests;

public class AuthorizedRequest<T> : PublicRequest<T>
{
    [JsonIgnore]
    public AuthorizeData? AuthorizeData { get; set; } = default!;
}

public class AuthorizeData
{
    public AuthorizeData(Guid userId, string role, string token)
    {
        UserId = userId;
        Role = role;
        Token = token;
    }

    public Guid UserId { get; set; }
    public string Role { get; set; }
    public string Token { get; set; }
}