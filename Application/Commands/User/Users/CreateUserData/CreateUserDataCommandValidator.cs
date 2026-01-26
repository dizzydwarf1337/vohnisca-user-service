using FluentValidation;

namespace Application.Commands.User.Users.CreateUserData;

public class CreateUserDataCommandValidator :  AbstractValidator<CreateUserDataCommand>
{
    public CreateUserDataCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithName("User id");
        RuleFor(x => x.UserName).NotEmpty().WithName("User name");
        RuleFor(x => x.UserMail).NotEmpty().WithName("User mail");
    }
}