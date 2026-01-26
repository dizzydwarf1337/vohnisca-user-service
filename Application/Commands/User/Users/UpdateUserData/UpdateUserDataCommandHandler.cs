using Application.Core.Extensions;
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
    
    public UpdateUserDataCommandHandler(IUserRepository userRepository)
        => _userRepository = userRepository;
    
    public async Task<Either<Error, Unit>> Handle(UpdateUserDataCommand request, CancellationToken cancellationToken)
    {
        return await CheckUserName(request.UserName, cancellationToken)
            .BindAsync(_ => GetUser(request.AuthorizeData!.UserId, cancellationToken))
            .BindAsync(u => u.UpdateUserData(request.UserName, request.Bio))
            .BindAsync(u => _userRepository.UpdateAsync(u, cancellationToken))
            .MapToUnitAsync();
    }

    private async Task<Either<Error, Domain.Models.Users.User>> GetUser(Guid userId, CancellationToken token)
    {
        return await _userRepository.GetByIdAsync(userId, token)
            .Match(
                Some: Prelude.Right<Error, Domain.Models.Users.User>,
                None: () => Prelude.Left<Error, Domain.Models.Users.User>(
                    Error.New("User not found")
                )
            );
    }

    private async Task<Either<Error, Unit>> CheckUserName(string userName, CancellationToken token)
    {
        var userNameExists =
            await _userRepository.GetAllEntities().AnyAsync(x => string.Equals(x.UserName, userName.Trim()), token);

        return userNameExists
            ? Error.New("Username already taken")
            : Unit.Default;
    }
}