using Application.Core.Mediatr.Requests;
using LanguageExt;

namespace Application.Commands.User.Users.UpdateUserData;

public class UpdateUserDataCommand : UserRequest<Unit>
{
    public string UserName { get; set; }
    
    public string Bio { get; set; }
}