using Application.Core.Extensions;
using Domain.Interfaces.Users;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace Application.Queries.User.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, Either<Error, GetUserQuery.User>>
{
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(IUserRepository userRepository)
        => _userRepository = userRepository;

    public async Task<Either<Error, GetUserQuery.User>> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var user = (await _userRepository.GetByIdAsync(query.Id, cancellationToken)).Value();

        if (user == null)
            return Error.New("User not found");

        var isFriends = user.Friends.Any(x => x.Id == query.AuthorizeData.UserId);

        if (user.UserSettings.IsPrivate && !isFriends)
            return Error.New("User account is private");

        return new GetUserQuery.User(user.Id, user.UserName, user.Bio, user.ProfilePicturePath, user.CreatedAt,
            isFriends);
    }
}