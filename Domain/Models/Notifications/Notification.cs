using Domain.Models.Notifications.Enums;
using Domain.Models.Users;
using LanguageExt;
using LanguageExt.Common;

namespace Domain.Models.Notifications;

public class Notification
{
    private Notification() { }
    
    public Guid Id { get; private set; } = Guid.NewGuid();
    
    public string Title { get; private set; } = string.Empty;
    
    public string Content { get; private set; } = string.Empty;

    public bool IsRead { get; private set; }
    
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    
    public DateTime? ReadAt { get; private set; }
    
    public NotificationType Type { get; private set; }
    
    public Guid? UserId { get; private set; }
    public virtual User? User { get; private set; } = null!;

    public static Either<Error, Notification> Create(
        Guid? userId,
        string title,
        string content,
        NotificationType type)
    {
        if (userId.HasValue && type == NotificationType.Global)
            return Error.New("Global notifications can't have user");
        if (!userId.HasValue && type != NotificationType.Global)
            return Error.New("Invalid user id");
        if (string.IsNullOrWhiteSpace(title) || title.Length > 100)
            return Error.New("Title is empty or contains more than 100 characters");
        if (string.IsNullOrWhiteSpace(content) || content.Length > 500)
            return Error.New("Content is empty or contains more than 500 characters");

        var notification = new Notification
        {
            UserId = userId,
            Title = title,
            Content = content,
            Type = type
        };

        return notification;
    }

    public Either<Error, Notification> MarkAsRead()
    {
        if (IsRead || Type == NotificationType.Global)
            return this;

        IsRead = true;
        ReadAt = DateTime.UtcNow;

        return this;
    }

    public Either<Error, Notification> MarkAsUnread()
    {
        if (!IsRead || Type == NotificationType.Global)
            return this;

        IsRead = false;
        ReadAt = null;

        return this;
    }
}