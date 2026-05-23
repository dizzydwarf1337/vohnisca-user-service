using Application.Core.Extensions;
using Domain.Interfaces.Users;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Unit = LanguageExt.Unit;

namespace Application.Commands.User.Friends.DeleteFriend;

public class DeleteFriendCommandHandler : IRequestHandler<DeleteFriendCommand, Either<Error, Unit>>
{
    private readonly IUserRepository _userRepository;

    public DeleteFriendCommandHandler(IUserRepository userRepository)
        => _userRepository = userRepository;

    public async Task<Either<Error, Unit>> Handle(DeleteFriendCommand request, CancellationToken cancellationToken)
    {
        return await GetUser(request.AuthorizeData.UserId, cancellationToken)
            .BindAsync(u => DeleteFriend(u, request.Id, cancellationToken))
            .MapToUnitAsync();
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
}