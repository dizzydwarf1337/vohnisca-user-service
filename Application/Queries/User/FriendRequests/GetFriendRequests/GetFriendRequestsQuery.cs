using Application.Core.Mediatr.Requests;
using Domain.Models.Users.Enums;

namespace Application.Queries.User.FriendRequests.GetFriendRequests;

public class GetFriendRequestsQuery : UserRequest<GetFriendRequestsQuery.Result>
{
    public record Result(FriendRequest[] FriendRequests);

    public record FriendRequest(Guid Id, string UserName, FriendRequestStatus Status, DateTime SentAt);
}