using Application.Events;
using Domain.Interfaces.Users;
using LanguageExt;
using LanguageExt.Common;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Unit = LanguageExt.Unit;

namespace Application.Commands.User.Friends.DeleteFriend;

public class DeleteFriendCommandHandler : IRequestHandler<DeleteFriendCommand, Either<Error, Unit>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public DeleteFriendCommandHandler(IUserRepository userRepository, IPublishEndpoint publishEndpoint)
    {
        _userRepository = userRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Either<Error, Unit>> Handle(DeleteFriendCommand request, CancellationToken cancellationToken)
    {
        return await GetUser(request.AuthorizeData.UserId, cancellationToken)
            .BindAsync(u => DeleteFriend(u, request.Id, cancellationToken))
            .BindAsync(u => Publish(u.Id, request.Id, cancellationToken));
    }

    private async Task<Either<Error, Domain.Models.Users.User>> GetUser(Guid userId, CancellationToken token)
    {
        var user = await _userRepository.GetAllEntities()
            .Include(x => x.Friends).SingleOrDefaultAsync(x => x.Id == userId, token);

        return user is null
            ? Error.New("User not found")
            : user;
    }

    private async Task<Either<Error, Domain.Models.Users.User>> DeleteFriend(Domain.Models.Users.User user,
        Guid friendId, CancellationToken token)
    {
        var friend = await _userRepository.GetAllEntities().Include(x => x.Friends)
            .SingleOrDefaultAsync(x => x.Id == friendId, token);

        if (friend == null)
            return Error.New("Friend not found");

        return await user.RemoveFriend(friend)
            .BindAsync(_ => _userRepository.UpdateAsync(user, token));
    }

    private async Task<Either<Error, Unit>> Publish(Guid firstUserId, Guid secondUserId, CancellationToken token)
    {
        await _publishEndpoint.Publish(new FriendRemovedEvent(firstUserId, secondUserId), token);
        return Unit.Default;
    }
}