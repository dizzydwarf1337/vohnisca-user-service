namespace Application.Consumers.Users.UserCreated;

public record UserCreatedEvent(string UserId, string UserName, string UserMail);