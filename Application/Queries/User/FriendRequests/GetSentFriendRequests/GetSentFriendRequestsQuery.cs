using Application.Core.Mediatr.Requests;
using Application.Core.Requests;
using Application.Core.Responses;
using Domain.Models.Users.Enums;

namespace Application.Queries.User.FriendRequests.GetSentFriendRequests;

public class GetSentFriendRequestsQuery : UserRequest<PaginationResponse<GetSentFriendRequestsQuery.FriendRequest>>
{
    public PaginationSpecification Pagination { get; set; }

    public record FriendRequest(
        Guid Id,
        string UserName,
        FriendRequestStatus Status,
        DateTime SentAt,
        DateTime? StatusChangedAt);
}