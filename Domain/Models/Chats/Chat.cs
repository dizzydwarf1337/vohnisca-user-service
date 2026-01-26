using Domain.Models.Users;
using LanguageExt;
using LanguageExt.Common;

namespace Domain.Models.Chats;

public class Chat
{
    private Chat() { }
    
    public Guid Id { get; private set; } = Guid.NewGuid();
    
    public string Name { get; private set; } = string.Empty;
    
    public string? Description { get; private set; }
    
    public string? AvatarUrl { get; private set; }
    
    public bool IsGroupChat { get; private set; }
    
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; private set; }
    
    public Guid CreatedById { get; private set; }
    public virtual User CreatedBy { get; set; } = null!;
    
    public Guid? PinnedMessageId { get; private set; }
    public virtual Message? PinnedMessage { get; set; }
    
    public bool IsDeleted { get; private set; } = false;
    public DateTime? DeletedAt { get; private set; } = null;
    
    public virtual ICollection<User> Participants { get; private set; } = new List<User>();
    public virtual ICollection<Message> Messages { get; private set; } = new List<Message>();
    
    public static Either<Error, Chat> CreatePrivateChat(
        User receiver, 
        User sender, 
        string name, 
        string? description = null,
        string? avatarUrl = null)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 50)
            return Error.New("Invalid chat name");
        if (description is { Length: > 500 })
            return Error.New("Invalid chat description");
        if (avatarUrl != null && string.IsNullOrWhiteSpace(avatarUrl))
            return Error.New("Invalid chat avatar url");
        if (receiver.Id == sender.Id)
            return Error.New("Cannot create chat with yourself");

        var chat = new Chat
        {
            Name = name,
            Description = description,
            AvatarUrl = avatarUrl,
            CreatedById = sender.Id,
            Participants = new List<User> { receiver, sender }
        };

        return chat;
    }

    public static Either<Error, Chat> CreateGroupChat(
        ICollection<User> participants, 
        User sender, 
        string name,
        string? description = null, 
        string? avatarUrl = null)
    {
        if (participants.Count == 0)
            return Error.New("No participants");
        if (string.IsNullOrWhiteSpace(name) || name.Length > 50)
            return Error.New("Invalid chat name");
        if (description is { Length: > 500 })
            return Error.New("Invalid chat description");
        if (avatarUrl != null && string.IsNullOrWhiteSpace(avatarUrl))
            return Error.New("Invalid chat avatar url");

        var allParticipants = new List<User>(participants) { sender };

        var chat = new Chat
        {
            Name = name,
            Description = description,
            AvatarUrl = avatarUrl,
            IsGroupChat = true,
            CreatedById = sender.Id,
            Participants = allParticipants
        };

        return chat;
    }

    public Either<Error, Chat> Update(string? name = null, string? description = null, string? avatarUrl = null)
    {
        if (name != null && (string.IsNullOrWhiteSpace(name) || name.Length > 50))
            return Error.New("Invalid chat name");
        if (description is { Length: > 500 })
            return Error.New("Invalid chat description");
        if (avatarUrl != null && string.IsNullOrWhiteSpace(avatarUrl))
            return Error.New("Invalid chat avatar url");

        if (name != null) Name = name;
        if (description != null) Description = description;
        if (avatarUrl != null) AvatarUrl = avatarUrl;
        UpdatedAt = DateTime.UtcNow;
        
        return this;
    }

    public Either<Error, Chat> AddParticipant(User user)
    {
        if (Participants.Any(p => p.Id == user.Id))
            return Error.New("User already in chat");
        
        Participants.Add(user);
        return this;
    }

    public Either<Error, Chat> RemoveParticipant(Guid userId)
    {
        if (userId == CreatedById)
            return Error.New("Cannot remove chat creator");
    
        var user = Participants.FirstOrDefault(p => p.Id == userId);
        if (user == null)
            return Error.New("User not in chat");
    
        Participants.Remove(user);
        return this;
    }

    public Either<Error, Chat> PinMessage(Guid messageId)
    {
        if (messageId == Guid.Empty)
            return Error.New("Invalid message id");
        
        PinnedMessageId = messageId;
        return this;
    }

    public Either<Error, Chat> UnpinMessage()
    {
        PinnedMessageId = null;
        return this;
    }

    public Either<Error, Chat> DeleteChat(Guid userId)
    {
        if (CreatedById != userId)
            return Error.New("Chat can be deleted only by creator");

        Name = "";
        Description = null;
        AvatarUrl = null;
        UpdatedAt = null;
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        Participants.Clear();
        Messages.Clear();

        return this;
    }
}