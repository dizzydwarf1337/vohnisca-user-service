using Application.Core.Responses;
using Domain.Interfaces.Users;
using Domain.Models.Users.Enums;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.User.FriendRequests.GetSentFriendRequests;

public class GetSentFriendRequestsQueryHandler : IRequestHandler<GetSentFriendRequestsQuery,
    Either<Error, PaginationResponse<GetSentFriendRequestsQuery.FriendRequest>>>
{
    private readonly IFriendRequestRepository _friendRequestRepository;
    private readonly IUserRepository _userRepository;

    public GetSentFriendRequestsQueryHandler(IFriendRequestRepository friendRequestRepository,
        IUserRepository userRepository)
    {
        _friendRequestRepository = friendRequestRepository;
        _userRepository = userRepository;
    }

    public async Task<Either<Error, PaginationResponse<GetSentFriendRequestsQuery.FriendRequest>>> Handle(
        GetSentFriendRequestsQuery request, CancellationToken cancellationToken)
    {
        var baseQuery = _friendRequestRepository.GetAllEntities()
            .Where(x =>
                x.SentBy == request.AuthorizeData.UserId &&
                x.Status != FriendRequestStatus.Deleted
            );

        var totalCount = await baseQuery.CountAsync(cancellationToken);
        var pagesCount = (int)Math.Ceiling(totalCount / (double)request.Pagination.PageSize);
        var hasNextPage = request.Pagination.Page < pagesCount;

        var friendRequests = await baseQuery
            .Skip(request.Pagination.Page - 1)
            .Take(request.Pagination.PageSize)
            .ToListAsync(cancellationToken);

        var senderIds = friendRequests.Select(fr => fr.SentTo).Distinct().ToList();

        var senders = await _userRepository.GetAllEntities()
            .Where(u => senderIds.Contains(u.Id))
            .Select(u => new { u.Id, u.UserName })
            .ToListAsync(cancellationToken);

        var senderDict = senders.ToDictionary(u => u.Id, u => u.UserName);

        var resultArray = friendRequests.Select(fr => new GetSentFriendRequestsQuery.FriendRequest(
            fr.Id,
            senderDict.GetValueOrDefault(fr.SentTo, "Unknown"),
            fr.Status,
            fr.SentAt,
            fr.StatusChangedAt
        )).ToArray();

        return new PaginationResponse<GetSentFriendRequestsQuery.FriendRequest>(request.Pagination.Page,
            request.Pagination.PageSize, hasNextPage, pagesCount, resultArray);
    }
}