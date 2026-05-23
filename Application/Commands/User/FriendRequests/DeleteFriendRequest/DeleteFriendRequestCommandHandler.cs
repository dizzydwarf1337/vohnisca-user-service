using Application.Core.Extensions;
using Domain.Interfaces.Users;
using Domain.Models.Users;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Unit = LanguageExt.Unit;

namespace Application.Commands.User.FriendRequests.DeleteFriendRequest;

public class DeleteFriendRequestCommandHandler : IRequestHandler<DeleteFriendRequestCommand, Either<Error, Unit>>
{
    private readonly IFriendRequestRepository _friendRequestRepository;

    public DeleteFriendRequestCommandHandler(IFriendRequestRepository friendRequestRepository)
    {
        _friendRequestRepository = friendRequestRepository;
    }

    public async Task<Either<Error, Unit>> Handle(DeleteFriendRequestCommand request,
        CancellationToken cancellationToken)
    {
        return await GetFriendRequest(request.Id, request.AuthorizeData.UserId, cancellationToken)
            .BindAsync(fr => fr.Delete())
            .BindAsync(fr => _friendRequestRepository.UpdateAsync(fr, cancellationToken))
            .MapToUnitAsync();
    }

    private async Task<Either<Error, FriendRequest>> GetFriendRequest(Guid id, Guid userId, CancellationToken token)
    {
        var request = (await _friendRequestRepository.GetByIdAsync(id, token)).Value();

        if (request == null)
            return Error.New("Friend request not found.");
        if (request.SentBy != userId && request.SentTo != userId)
            return Error.New("Friend request not found.");

        return request;
    }
}