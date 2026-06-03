using Application.Core.Mediatr.Requests;

namespace Application.Queries.User.GetUser;

public class GetUserQuery : UserRequest<GetUserQuery.User>
{
    public Guid Id { get; set; }

    public record User(
        Guid Id,
        string UserName,
        string Bio,
        string ProfilePicturePath,
        DateTime CreatedAt,
        bool IsFriends);
}