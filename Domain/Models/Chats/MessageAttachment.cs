using Domain.Models.Chats.Enums;
using LanguageExt;
using LanguageExt.Common;

namespace Domain.Models.Chats;

public class MessageAttachment
{
    private MessageAttachment() { }
    
    public Guid Id { get; private set; } = Guid.NewGuid();
    
    public Guid MessageId { get; private set; }
    public virtual Message Message { get; private set; } = null!;
    
    public string FileName { get; private set; } = string.Empty;
    
    public string FileUrl { get; private set; } = string.Empty;
    
    public AttachmentType Type { get; private set; }
    
    public long FileSize { get; private set; }
    
    public string? MimeType { get; private set; }
    
    public int? Width { get; private set; }
    public int? Height { get; private set; }
    
    public int? Duration { get; private set; }
    
    public DateTime UploadedAt { get; private set; } = DateTime.UtcNow;

    public static Either<Error, MessageAttachment> Create(
        Guid messageId,
        string fileName,
        string fileUrl,
        AttachmentType type,
        long fileSize,
        string? mimeType = null,
        int? width = null,
        int? height = null,
        int? duration = null)
    {
        if (messageId == Guid.Empty)
            return Error.New("Invalid message id");
        if (string.IsNullOrWhiteSpace(fileName) || fileName.Length > 255)
            return Error.New("Invalid file name");
        if (string.IsNullOrWhiteSpace(fileUrl) || fileUrl.Length > 1000)
            return Error.New("Invalid file url");
        if (fileSize <= 0)
            return Error.New("Invalid file size");
        if (width is < 0)
            return Error.New("Invalid width");
        if (height is < 0)
            return Error.New("Invalid height");
        if (duration is < 0)
            return Error.New("Invalid duration");

        var attachment = new MessageAttachment
        {
            MessageId = messageId,
            FileName = fileName,
            FileUrl = fileUrl,
            Type = type,
            FileSize = fileSize,
            MimeType = mimeType,
            Width = width,
            Height = height,
            Duration = duration
        };

        return attachment;
    }
}