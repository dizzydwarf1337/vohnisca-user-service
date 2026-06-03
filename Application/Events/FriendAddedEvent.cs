using MassTransit;

namespace Application.Events;

[EntityName("friend-added")]
public record class FriendAddedEvent(Guid FirstUserId, Guid SecondUserId);