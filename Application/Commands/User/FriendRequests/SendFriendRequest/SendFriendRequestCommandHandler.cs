using Application.Core.Extensions;
using Domain.Interfaces.Users;
using Domain.Models.Users;
using Domain.Models.Users.Enums;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Unit = LanguageExt.Unit;

namespace Application.Commands.User.FriendRequests.SendFriendRequest;

public class SendFriendRequestCommandHandler : IRequestHandler<SendFriendRequestCommand, Either<Error, Unit>>
{
    private readonly IFriendRequestRepository _friendRequestRepository;
    private readonly IUserRepository _userRepository;

    public SendFriendRequestCommandHandler(IFriendRequestRepository friendRequestRepository,
        IUserRepository userRepository)
    {
        _friendRequestRepository = friendRequestRepository;
        _userRepository = userRepository;
    }
    
    public async Task<Either<Error, Unit>> Handle(SendFriendRequestCommand request, CancellationToken cancellationToken)
    {
        return await CheckUserExists(request.UserName, cancellationToken)
            .BindAsync(id => CheckRequestExists(request.AuthorizeData!.UserId, id, cancellationToken))
            .BindAsync(id => FriendRequest.Create(request.AuthorizeData!.UserId, id))
            .BindAsync(fr => _friendRequestRepository.SaveAsync(fr, cancellationToken))
            .MapToUnitAsync();
    }

    private async Task<Either<Error, Guid>> CheckUserExists(string userName, CancellationToken token)
    {
        var user = await _userRepository.GetAllEntities()
            .FirstOrDefaultAsync(
                x => string.Equals(x.UserName, userName, StringComparison.OrdinalIgnoreCase) &&
                     x.UserSettings.Status == UserStatus.Activated, token);

        return user is not null
            ? user.Id
            : Error.New("User does not exist or has been blocked");
    }

    private async Task<Either<Error, Guid>> CheckRequestExists(Guid sentBy, Guid sentTo, CancellationToken token)
    {
        var request = await _friendRequestRepository.GetAllEntities().FirstOrDefaultAsync(
            x => (x.SentBy == sentBy && x.SentTo == sentTo) ||
                 (x.SentBy == sentTo && x.SentTo == sentBy) && x.Status == FriendRequestStatus.Pending,
            token);

        return request is null
            ? sentTo
            : Error.New("Pending friend request already exists");
    }
}