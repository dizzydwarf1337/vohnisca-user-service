using Application.Commands.Admin.Notifications.SendNotification;
using Application.Commands.User.Users.CreateUserData;
using Application.Core.Extensions;
using LanguageExt;
using LanguageExt.Common;
using MassTransit;
using MediatR;
using Unit = LanguageExt.Unit;

namespace Application.Consumers.Users.UserCreated;

public class UserCreatedConsumer : IConsumer<UserCreatedEvent>
{
    private readonly IMediator _mediator;
    
    public UserCreatedConsumer(IMediator mediator) => _mediator = mediator;
    
    public async Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        await CreateUserData(context)
            .BindAsync(_ => SendNotification(context))
            .LogAsync("MassTransit");
    }

    private async Task<Either<Error, Unit>> CreateUserData(ConsumeContext<UserCreatedEvent> context)
    {
        if (!Guid.TryParse(context.Message.UserId, out var userId))
            return Error.New("Invalid user id");
        
        var createUserDataCommand = new CreateUserDataCommand
        {
            IsSystemRequest = true,
            UserId = userId,
            UserName = context.Message.UserName,
            UserMail = context.Message.UserMail
        };
        return await _mediator.Send(createUserDataCommand);
    }

    private async Task<Either<Error, Unit>> SendNotification(ConsumeContext<UserCreatedEvent> context)
    {
        if (!Guid.TryParse(context.Message.UserId, out var userId))
            return Error.New("Invalid user id");

        var sendNotificationCommand = new SendNotificationCommand()
        {
            IsSystemRequest = true,
            UserId = userId,
            Title = "Account created successfully",
            Content = "Thank you for joining Vohnisca! Your account was created successfully and now you have access to all features! " +
                      "Write your own story or join other players — it's all up to you. Let the battle begin!"
        };
        return await _mediator.Send(sendNotificationCommand);
    }
}