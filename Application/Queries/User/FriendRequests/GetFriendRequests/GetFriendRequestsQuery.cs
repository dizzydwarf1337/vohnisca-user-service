using Application.Core.Mediatr.Requests;
using Domain.Models.Users.Enums;

namespace Application.Queries.User.FriendRequests.GetFriendRequests;

public class GetFriendRequestsQuery : UserRequest<IEnumerable<GetFriendRequestsQuery.FriendRequest>>
{
    public record FriendRequest(Guid Id, string UserName, FriendRequestStatus Status, DateTime SentAt);
}