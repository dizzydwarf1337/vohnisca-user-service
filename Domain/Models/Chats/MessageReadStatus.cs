using Domain.Models.Users;
using LanguageExt;
using LanguageExt.Common;

namespace Domain.Models.Chats;

public class MessageReadStatus
{
    private MessageReadStatus() { }

    public Guid MessageId { get; private set; }
    public virtual Message Message { get; private set; } = null!;
    
    public Guid UserId { get; private set; }
    public virtual User User { get; private set; } = null!;
    
    public DateTime ReadAt { get; private set; } = DateTime.UtcNow;

    public static Either<Error, MessageReadStatus> Create(Guid messageId, Guid userId)
    {
        if (messageId == Guid.Empty)
            return Error.New("Invalid message id");
        if (userId == Guid.Empty)
            return Error.New("Invalid user id");

        var status = new MessageReadStatus
        {
            MessageId = messageId,
            UserId = userId
        };

        return status;
    }
}