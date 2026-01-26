using Application.Core.Mediatr.Requests;
using Unit = LanguageExt.Unit;

namespace Application.Commands.User.Users.CreateUserData;

public class CreateUserDataCommand : PublicRequest<Unit>
{
    public Guid UserId { get; set; }
    public string UserName  { get; set; }
    public string UserMail { get; set; }
}