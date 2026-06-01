using Application.Core.Mediatr.Requests;

namespace Application.Queries.User.Me;

public class GetMeQuery : UserRequest<GetMeQuery.Result>
{
    public record Result(
        Guid Id,
        string UserName,
        string Email,
        string Bio,
        DateTime CreatedAt,
        bool HasUnreadNotifications,
        bool HasUnreadMessages,
        int FriendsOnline
        );
}