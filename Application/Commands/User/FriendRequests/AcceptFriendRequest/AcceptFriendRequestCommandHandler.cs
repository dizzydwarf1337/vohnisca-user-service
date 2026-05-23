using Application.Core.Extensions;
using Domain.Interfaces.Users;
using Domain.Models.Users;
using Domain.Models.Users.Enums;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Unit = LanguageExt.Unit;

namespace Application.Commands.User.FriendRequests.AcceptFriendRequest;

public class AcceptFriendRequestCommandHandler : IRequestHandler<AcceptFriendRequestCommand, Either<Error, Unit>>
{
    private readonly IFriendRequestRepository _friendRequestRepository;
    private readonly IUserRepository _userRepository;

    public AcceptFriendRequestCommandHandler(IFriendRequestRepository friendRequestRepository,
        IUserRepository userRepository)
    {
        _friendRequestRepository = friendRequestRepository;
        _userRepository = userRepository;
    }

    public async Task<Either<Error, Unit>> Handle(AcceptFriendRequestCommand request,
        CancellationToken cancellationToken)
    {
        return await GetFriendRequest(request.Id, request.AuthorizeData.UserId, cancellationToken)
            .BindAsync(fr => fr.Accept())
            .BindAsync(fr => _friendRequestRepository.UpdateAsync(fr, cancellationToken))
            .BindAsync(fr => AddFriends(fr.SentBy, fr.SentTo, cancellationToken))
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

    private async Task<Either<Error, Unit>> AddFriends(Guid firstId, Guid secondId, CancellationToken token)
    {
        var firstUser = await _userRepository.GetAllEntities().Include(x => x.Friends)
            .FirstOrDefaultAsync(x => x.Id == firstId, token);
        var secondUser = await _userRepository.GetAllEntities().Include(x => x.Friends)
            .FirstOrDefaultAsync(x => x.Id == secondId, token);

        if (firstUser == null || secondUser == null)
            return Error.New("Error while adding new friend");

        return await firstUser.AddFriend(secondUser)
            .BindAsync(_ => _userRepository.UpdateAsync(firstUser, token))
            .MapToUnitAsync();
    }
}