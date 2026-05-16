using Application.Core.Extensions;
using Domain.Interfaces.Users;
using Domain.Models.Users;
using Domain.Models.Users.Enums;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Unit = LanguageExt.Unit;

namespace Application.Commands.User.FriendRequests.AcceptFriendRequest;

public class AcceptFriendRequestCommandHandler : IRequestHandler<AcceptFriendRequestCommand, Either<Error, Unit>>
{
    private readonly IFriendRequestRepository _friendRequestRepository;

    public AcceptFriendRequestCommandHandler(IFriendRequestRepository friendRequestRepository)
        => _friendRequestRepository = friendRequestRepository;

    public async Task<Either<Error, Unit>> Handle(AcceptFriendRequestCommand request,
        CancellationToken cancellationToken)
    {
        return await GetFriendRequest(request.Id, request.AuthorizeData.UserId, cancellationToken)
            .BindAsync(fr => fr.Accept())
            .BindAsync(fr => _friendRequestRepository.UpdateAsync(fr, cancellationToken))
            .MapToUnitAsync();
    }

    private async Task<Either<Error, FriendRequest>> GetFriendRequest(Guid id, Guid userId, CancellationToken token)
    {
        var request = (await _friendRequestRepository.GetByIdAsync(id, token)).Value();

        if (request == null)
            return Error.New("Friend request does not exist");
        if (request.Status != FriendRequestStatus.Pending)
            return Error.New("Request was accepted or rejected");
        if (request.SentTo != userId)
            return Error.New("Friend request not found");

        return request;
    }
}