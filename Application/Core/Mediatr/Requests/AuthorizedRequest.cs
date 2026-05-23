using System.Text.Json.Serialization;

namespace Application.Core.Mediatr.Requests;

public class AuthorizedRequest<T> : PublicRequest<T>
{
    [JsonIgnore]
    public AuthorizeData AuthorizeData { get; set; } = default!;
}

public class AuthorizeData
{
    public AuthorizeData(Guid userId, string role, string token)
    {
        UserId = userId;
        Role = role;
        Token = token;
    }

    [JsonIgnore]
    public Guid UserId { get; set; }
    
    [JsonIgnore]
    public string Role { get; set; }
    
    [JsonIgnore]
    public string Token { get; set; }
}