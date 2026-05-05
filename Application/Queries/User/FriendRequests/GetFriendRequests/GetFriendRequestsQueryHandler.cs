using Domain.Interfaces.Users;
using Domain.Models.Users;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.User.FriendRequests.GetFriendRequests;

public class GetFriendRequestsQueryHandler : IRequestHandler<GetFriendRequestsQuery, Either<Error, GetFriendRequestsQuery.Result>>
{
    private readonly IUserRepository _userRepository;
    
    public GetFriendRequestsQueryHandler(IUserRepository userRepository)
        => _userRepository = userRepository;
    
    public async Task<Either<Error, GetFriendRequestsQuery.Result>> Handle(GetFriendRequestsQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetByIdAsync(request.AuthorizeData!.UserId, cancellationToken)
            .ToEitherAsync(Error.New("User not found"))
            .MapAsync(u => ToResult(u.ReceivedFriendRequests, cancellationToken));
    }

    public async Task<GetFriendRequestsQuery.Result> ToResult(ICollection<FriendRequest> friendRequests,
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