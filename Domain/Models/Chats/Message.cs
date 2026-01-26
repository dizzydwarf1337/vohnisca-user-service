using Domain.Models.Users;
using LanguageExt;
using LanguageExt.Common;

namespace Domain.Models.Chats;

public class Message
{
    private Message() { }

    public Guid Id { get; private set; } = Guid.NewGuid();
    
    public Guid ChatId { get; private set; }
    public virtual Chat Chat { get; private set; } = null!;
    
    public Guid SenderId { get; private set; }
    public virtual User Sender { get; private set; } = null!;
    
    public string? Content { get; private set; }
    
    public DateTime SentAt { get; private set; } = DateTime.UtcNow;
    
    public bool IsEdited { get; private set; }
    public DateTime? EditedAt { get; private set; }
    
    public bool IsDeleted { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    
    public Guid? ReplyToMessageId { get; private set; }
    public virtual Message? ReplyToMessage { get; private set; }

    public virtual ICollection<MessageAttachment> Attachments { get; private set; } = new List<MessageAttachment>();
    public virtual ICollection<MessageReadStatus> ReadStatuses { get; private set; } = new List<MessageReadStatus>();

    public static Either<Error, Message> Create(
        Guid chatId,
        Guid senderId,
        string? content = null,
        Guid? replyToMessageId = null)
    {
        if (chatId == Guid.Empty)
            return Error.New("Invalid chat id");
        if (senderId == Guid.Empty)
            return Error.New("Invalid sender id");
        if (string.IsNullOrWhiteSpace(content) && replyToMessageId == null)
            return Error.New("Message must have content or be a reply");
        if (content is { Length: > 4000 })
            return Error.New("Message content too long");

        var message = new Message
        {
            ChatId = chatId,
            SenderId = senderId,
            Content = content,
            ReplyToMessageId = replyToMessageId
        };

        return message;
    }

    public Either<Error, Message> Edit(string content)
    {
        if (IsDeleted)
            return Error.New("Cannot edit deleted message");
        if (string.IsNullOrWhiteSpace(content))
            return Error.New("Content cannot be empty");
        if (content.Length > 4000)
            return Error.New("Message content too long");

        Content = content;
        IsEdited = true;
        EditedAt = DateTime.UtcNow;

        return this;
    }

    public Either<Error, Message> Delete()
    {
        if (IsDeleted)
            return Error.New("Message already deleted");

        Content = null;
        Attachments.Clear();
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;

        return this;
    }

    public Either<Error, Message> AddAttachment(MessageAttachment attachment)
    {
        if (IsDeleted)
            return Error.New("Cannot add attachment to deleted message");
        
        Attachments.Add(attachment);
        return this;
    }

    public Either<Error, Message> MarkAsRead(Guid userId)
    {
        if (userId == Guid.Empty)
            return Error.New("Invalid user id");
        if (ReadStatuses.Any(rs => rs.UserId == userId))
            return Error.New("Message already read by user");

        return MessageReadStatus.Create(Id, userId)
            .Map(status =>
            {
                ReadStatuses.Add(status);
                return this;
            });
    }
}