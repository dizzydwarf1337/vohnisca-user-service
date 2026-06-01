using Application.Core.Mediatr.Requests;
using Application.Core.Requests;
using Application.Core.Responses;

namespace Application.Queries.User.FriendRequests.GetFriends;

public class GetFriendsQuery : UserRequest<PaginationResponse<GetFriendsQuery.Friend>>
{
    public PaginationSpecification Pagination { get; set; }

    public record Friend(Guid Id, string UserName, string ProfilePicturePath, DateTime? LastSeen);
}