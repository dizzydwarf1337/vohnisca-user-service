using Application.Core.Storage;
using Application.Events;
using Application.Interfaces.Storage;
using Domain.Interfaces.Users;
using LanguageExt;
using LanguageExt.Common;
using MassTransit;
using MediatR;
using Unit = LanguageExt.Unit;

namespace Application.Commands.User.Users.DeleteProfilePicture;

public class DeleteProfilePictureCommandHandler : IRequestHandler<DeleteProfilePictureCommand, Either<Error, Unit>>
{
    private readonly IUserRepository _userRepository;
    private readonly IBlobStorage _blobStorage;
    private readonly IPublishEndpoint _publishEndpoint;

    public DeleteProfilePictureCommandHandler(IUserRepository userRepository, IBlobStorage blobStorage,
        IPublishEndpoint publishEndpoint)
    {
        _userRepository = userRepository;
        _blobStorage = blobStorage;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Either<Error, Unit>> Handle(DeleteProfilePictureCommand command,
        CancellationToken cancellationToken)
    {
        return await GetUser(command.AuthorizeData.UserId, cancellationToken)
            .BindAsync(u => DeleteProfilePicture(u, cancellationToken))
            .BindAsync(u => _userRepository.UpdateAsync(u, cancellationToken))
            .BindAsync(u => Publish(u, cancellationToken));
    }

    private async Task<Either<Error, Domain.Models.Users.User>> GetUser(Guid userId, CancellationToken token)
    {
        return await _userRepository.GetByIdAsync(userId, token)
            .ToEitherAsync(Error.New("User not found"));
    }

    private async Task<Either<Error, Domain.Models.Users.User>> DeleteProfilePicture(Domain.Models.Users.User user,
        CancellationToken token)
        => await _blobStorage.DeleteFileAsync(StorageFileKey.ExtractKeyFromUrl(user.ProfilePicturePath), token)
            .MapAsync(_ => user)
            .BindAsync(u => u.SetProfilePicture(string.Empty));

    private async Task<Either<Error, Unit>> Publish(Domain.Models.Users.User user, CancellationToken token)
    {
        await _publishEndpoint.Publish(new UserProfileDataChangedEvent(user.Id, user.UserName, user.ProfilePicturePath,
            user.UserSettings.IsPrivate), token);

        return Unit.Default;
    }
}