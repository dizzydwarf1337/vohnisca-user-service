using Application.Core.Mediatr.Requests;

namespace Application.Queries.User.Me;

public class GetMeQuery : UserRequest<GetMeQuery.Result>
{
    public record Result(
        string UserName,
        string Email,
        string Bio,
        DateTime CreatedAt,
        int UnreadNotificationsCount,
        int UnreadMessagesCount,
        int FriendsOnline
        );
}