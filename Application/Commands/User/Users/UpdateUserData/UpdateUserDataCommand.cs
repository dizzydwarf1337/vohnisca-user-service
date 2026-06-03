using Application.Core.Mediatr.Requests;
using LanguageExt;

namespace Application.Commands.User.Users.UpdateUserData;

public class UpdateUserDataCommand : UserRequest<Unit>
{
    public Request UserData { get; set; }

    public class Request
    {
        public string UserName { get; set; }

        public string Bio { get; set; }

        public bool IsPrivate { get; set; }

        public byte[] ProfilePicture { get; init; }

        public string? ProfilePictureContentType { get; init; }
    }
}