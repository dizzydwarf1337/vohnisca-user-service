using Application.Core.Mediatr.Requests;

namespace Application.Queries.User.Me;

public class GetMeQuery : UserRequest<GetMeQuery.Result>
{
    public record Result(
        Guid Id,
        string UserName,
        string Email,
        string Bio,
        string ProfilePicturePath,
        DateTime CreatedAt,
        bool IsPrivate,
        bool HasUnreadNotifications,
        bool HasUnreadMessages,
        int FriendsOnline
    );
}