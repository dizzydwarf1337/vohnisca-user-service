using Domain.Interfaces.Users;
using Domain.Models.Users;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.User.FriendRequests.GetFriendRequests;

public class
    GetFriendRequestsQueryHandler : IRequestHandler<GetFriendRequestsQuery,
    Either<Error, GetFriendRequestsQuery.Result>>
{
    private readonly IUserRepository _userRepository;

    public GetFriendRequestsQueryHandler(IUserRepository userRepository)
        => _userRepository = userRepository;

    public async Task<Either<Error, GetFriendRequestsQuery.Result>> Handle(GetFriendRequestsQuery request,
        CancellationToken cancellationToken)
    {
        return await GetUser(request.AuthorizeData.UserId, cancellationToken)
            .MapAsync(u => ToResult(u.ReceivedFriendRequests, cancellationToken));
    }

    private async Task<Either<Error, Domain.Models.Users.User>> GetUser(Guid userId, CancellationToken token)
    {
        var user = await _userRepository
            .GetAllEntities()
            .Include(x => x.ReceivedFriendRequests)
            .FirstOrDefaultAsync(x => x.Id == userId, token);

        if (user == null)
            return Error.New("User not found");

        return user;
    }

    private async Task<GetFriendRequestsQuery.Result> ToResult(ICollection<FriendRequest> friendRequests,
        CancellationToken cancellationToken)
    {
        var senderIds = friendRequests.Select(fr => fr.SentBy).Distinct().ToList();

        var senders = await _userRepository.GetAllEntities()
            .Where(u => senderIds.Contains(u.Id))
            .Select(u => new { u.Id, u.UserName })
            .ToListAsync(cancellationToken);

        var senderDict = senders.ToDictionary(u => u.Id, u => u.UserName);

        var resultArray = friendRequests.Select(fr => new GetFriendRequestsQuery.FriendRequest(
            fr.Id,
            senderDict.GetValueOrDefault(fr.SentBy, "Unknown"),
            fr.Status,
            fr.SentAt
        )).ToArray();

        return new GetFriendRequestsQuery.Result(resultArray);
    }
}