using Application.Core.Extensions;
using Domain.Interfaces.Users;
using Domain.Models.Users;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Unit = LanguageExt.Unit;

namespace Application.Commands.User.FriendRequests.CancelFriendRequest;

public class CancelFriendRequestCommandHandler : IRequestHandler<CancelFriendRequestCommand, Either<Error, Unit>>
{
    private readonly IFriendRequestRepository _friendRequestRepository;

    public CancelFriendRequestCommandHandler(IFriendRequestRepository friendRequestRepository)
        => _friendRequestRepository = friendRequestRepository;

    public async Task<Either<Error, Unit>> Handle(CancelFriendRequestCommand request,
        CancellationToken cancellationToken)
    {
        return await GetFriendRequest(request.Id, request.AuthorizeData.UserId, cancellationToken)
            .BindAsync(fr => fr.Cancel())
            .BindAsync(fr => _friendRequestRepository.UpdateAsync(fr, cancellationToken))
            .MapToUnitAsync();
    }

    private async Task<Either<Error, FriendRequest>> GetFriendRequest(Guid id, Guid userId, CancellationToken token)
    {
        var request = (await _friendRequestRepository.GetByIdAsync(id, token)).Value();

        if (request == null || request.SentBy != userId)
            return Error.New("Friend request not found.");

        return request;
    }
}