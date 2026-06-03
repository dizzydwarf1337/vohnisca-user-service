using MassTransit;

namespace Application.Events;

[EntityName("user-profile-data-changed")]
public record UserProfileDataChangedEvent(Guid Id, string UserName, string ProfilePicturePath, bool IsPrivate);