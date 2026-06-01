using Application.Core.Extensions;
using Application.Core.Storage;
using Application.Interfaces.Storage;
using Domain.Interfaces.Users;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Unit = LanguageExt.Unit;

namespace Application.Commands.User.Users.UpdateUserData;

public class UpdateUserDataCommandHandler : IRequestHandler<UpdateUserDataCommand, Either<Error, Unit>>
{
    private readonly IUserRepository _userRepository;
    private readonly IBlobStorage _blobStorage;

    public UpdateUserDataCommandHandler(IUserRepository userRepository, IBlobStorage blobStorage)
    {
        _userRepository = userRepository;
        _blobStorage = blobStorage;
    }

    public async Task<Either<Error, Unit>> Handle(UpdateUserDataCommand command, CancellationToken cancellationToken)
    {
        return await CheckUserName(command.UserData.UserName, command.AuthorizeData.UserId, cancellationToken)
            .BindAsync(_ => GetUser(command.AuthorizeData.UserId, cancellationToken))
            .BindAsync(u => UploadProfilePicture(command, u, cancellationToken))
            .BindAsync(u => u.UpdateUserData(command.UserData.UserName, command.UserData.Bio))
            .BindAsync(u => u.SetProfileVisibility(command.UserData.IsPrivate))
            .BindAsync(u => _userRepository.UpdateAsync(u, cancellationToken))
            .MapToUnitAsync();
    }

    private async Task<Either<Error, Unit>> CheckUserName(string userName, Guid userId, CancellationToken token)
    {
        var userNameExists =
            await _userRepository.GetAllEntities()
                .AnyAsync(x => string.Equals(x.UserName, userName.Trim()) && x.Id != userId, token);

        return userNameExists
            ? Error.New("Username already taken")
            : Unit.Default;
    }

    private async Task<Either<Error, Domain.Models.Users.User>> GetUser(Guid userId, CancellationToken token)
    {
        return await _userRepository.GetByIdAsync(userId, token)
            .ToEitherAsync(Error.New("User not found"));
    }

    private async Task<Either<Error, Domain.Models.Users.User>> UploadProfilePicture(
        UpdateUserDataCommand request,
        Domain.Models.Users.User user,
        CancellationToken token)
    {
        if (request.UserData.ProfilePicture is null or { Length: 0 })
            return user;

        if (!string.IsNullOrEmpty(user.ProfilePicturePath))
        {
            var existingKey = StorageFileKey.ExtractKeyFromUrl(user.ProfilePicturePath);
            await _blobStorage.DeleteFileAsync(existingKey, token);
        }

        using var stream = new MemoryStream(request.UserData.ProfilePicture);

        return await _blobStorage.SaveFileAsync(new BlobUploadRequest(
                Key: $"avatars/{user.Id}-{Guid.NewGuid()}",
                Content: stream,
                ContentType: request.UserData.ProfilePictureContentType ?? "image/jpeg"
            ), token)
            .BindAsync(uri => user.SetProfilePicture(uri.ToString()));
    }
}