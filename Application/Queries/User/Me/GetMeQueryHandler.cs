using Domain.Interfaces.Users;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace Application.Queries.User.Me;

public class GetMeQueryHandler : IRequestHandler<GetMeQuery, Either<Error, GetMeQuery.Result>>
{
    private readonly IUserRepository _repository;
    
    public GetMeQueryHandler(IUserRepository repository)
        => _repository = repository;
    
    public async Task<Either<Error, GetMeQuery.Result>> Handle(GetMeQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.AuthorizeData!.UserId, cancellationToken)
            .ToEitherAsync(Error.New(404, "User nor found"))
            .Bind(u => u.Seen().ToAsync())
            .Map(FillUserData);
    }

    private GetMeQuery.Result FillUserData(Domain.Models.Users.User user)
    {
        return new GetMeQuery.Result(
            user.UserName,
            user.Email,
            user.Bio,
            user.CreatedAt,
            user.Notifications.Any(x => !x.IsRead), 
            user.Chats.Any(c => c.Messages.Any(m => m.ReadStatuses.All(rs => rs.UserId != user.Id))),
            user.Friends.Count(x => x.LastSeenAt > DateTime.UtcNow.AddMinutes(-5))
        );
    }
}