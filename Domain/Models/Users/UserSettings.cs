using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Users.Enums;
using LanguageExt;
using LanguageExt.Common;

namespace Domain.Models.Users;

public class UserSettings
{
    protected UserSettings() { }
    
    [Key]
    public Guid Id { get; private set; } =  Guid.NewGuid();
    
    public DateTime? ActivatedAt { get; private set; }
    
    public DateTime? BlockedAt { get; private set; }
    
    public DateTime? DeletedAt { get; private set; }

    public UserStatus Status { get; private set; }
    
    public Guid UserId { get; init; }
    
    [ForeignKey(nameof(UserId))]
    public virtual User User { get; init; }

    public static Either<Error, UserSettings> Create(Guid userId)
        => new UserSettings()
        {
            UserId = userId,
            Status = UserStatus.RequiredActivate
        };

    public Either<Error, UserSettings> Activate()
    {
        if (Status == UserStatus.Activated)
            return Error.New("User already active");
        
        Status = UserStatus.Activated;
        ActivatedAt = DateTime.UtcNow;
        BlockedAt = null;
        DeletedAt = null;
        return this;
    }

    public Either<Error, UserSettings> Block()
    {
        if (Status == UserStatus.Blocked)
            return Error.New("User already blocked");
        Status = UserStatus.Blocked;
        BlockedAt = DateTime.UtcNow;
        ActivatedAt = null;
        DeletedAt = null;
        return this;
    }

    public Either<Error, UserSettings> Delete()
    {
        if (Status == UserStatus.Deleted)
            return Error.New("User already deleted");
        Status = UserStatus.Deleted;
        DeletedAt = DateTime.UtcNow;
        BlockedAt = null;
        ActivatedAt = null;
        return this;
    }
    
    
}