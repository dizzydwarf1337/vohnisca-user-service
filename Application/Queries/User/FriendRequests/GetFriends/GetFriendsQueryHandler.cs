using Application.Core.Responses;
using Domain.Interfaces.Users;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.User.FriendRequests.GetFriends;

public class
    GetFriendsQueryHandler : IRequestHandler<GetFriendsQuery, Either<Error, PaginationResponse<GetFriendsQuery.Friend>>>
{
    private readonly IUserRepository _userRepository;

    public GetFriendsQueryHandler(IUserRepository userRepository)
        => _userRepository = userRepository;

    public async Task<Either<Error, PaginationResponse<GetFriendsQuery.Friend>>> Handle(GetFriendsQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAllEntities()
            .Include(u => u.Friends)
            .FirstOrDefaultAsync(u => u.Id == request.AuthorizeData.UserId, cancellationToken);

        if (user is null)
            return Error.New("User not found");

        var totalCount = user.Friends.Count;
        var pagesCount = (int)Math.Ceiling(totalCount / (double)request.Pagination.PageSize);
        var hasNextPage = request.Pagination.Page < pagesCount;

        var friends = user.Friends
            .OrderByDescending(f => f.Id)
            .Skip((request.Pagination.Page - 1) * request.Pagination.PageSize)
            .Take(request.Pagination.PageSize)
            .Select(f => new GetFriendsQuery.Friend(f.Id, f.UserName, f.LastSeenAt))
            .ToArray();

        return new PaginationResponse<GetFriendsQuery.Friend>(
            request.Pagination.Page,
            request.Pagination.PageSize,
            hasNextPage,
            pagesCount,
            friends
        );
    }
}