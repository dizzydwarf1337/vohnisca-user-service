using MassTransit;

namespace Application.Events;

[EntityName("friend-removed")]
public record FriendRemovedEvent(Guid FirstUserId, Guid SecondUserId);