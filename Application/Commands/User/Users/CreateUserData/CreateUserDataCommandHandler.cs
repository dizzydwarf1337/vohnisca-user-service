using Application.Core.Extensions;
using Domain.Interfaces.Users;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Unit = LanguageExt.Unit;

namespace Application.Commands.User.Users.CreateUserData;

public class CreateUserDataCommandHandler : IRequestHandler<CreateUserDataCommand, Either<Error, Unit>>
{
    private readonly IUserRepository _userRepository;
    
    public CreateUserDataCommandHandler(IUserRepository userRepository)
        => _userRepository = userRepository;
    
    public async Task<Either<Error, Unit>> Handle(CreateUserDataCommand command, CancellationToken cancellationToken)
    {
        return await CheckUserExists(command.UserId, cancellationToken)
            .BindAsync(_ => Domain.Models.Users.User.Create(command.UserName, command.UserMail, "", command.UserId))
            .BindAsync(u => u.Activate())
            .BindAsync(u => _userRepository.SaveAsync(u, cancellationToken))
            .MapToUnitAsync();
    }
    
    private async Task<Either<Error, Unit>> CheckUserExists(Guid userId, CancellationToken token)
    {
        var user = await _userRepository.GetByIdAsync(userId, token);
        if (user.IsSome)
            return Error.New("User already exists");

        return Unit.Default;
    }
}