using MassTransit;

namespace Application.Consumers.Users.UserCreated;

[EntityName("user-created")]
public record UserCreatedEvent(string UserId, string UserName, string UserMail);