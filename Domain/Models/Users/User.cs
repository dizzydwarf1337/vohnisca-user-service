using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Users.Enums;
using LanguageExt;
using LanguageExt.Common;

namespace Domain.Models.Users;

public class User
{
    protected User() { }
    
    [Key] public Guid Id { get; private set; } = Guid.NewGuid();

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    
    public DateTime? LastSeenAt { get; private set; }
    
    public DateTime? UpdatedAt { get; private set; }
    
    public string UserName { get; private set; }

    public string Email { get; private set; }
    
    public string Bio { get; private set; }
    
    public virtual ICollection<User> Friends { get; set; } = new List<User>();
    
    public UserSettings UserSettings { get; init; } 

    public static Either<Error, User> Create(string userName, string email, string bio)
    {
        if (string.IsNullOrWhiteSpace(userName)) return Error.New("Invalid username");
        if (string.IsNullOrWhiteSpace(email)) return Error.New("Invalid email");
        if (string.IsNullOrWhiteSpace(bio)) return Error.New("Invalid bio");

        return new User
        {
            UserName = userName.Trim(),
            Email = email.Trim(),
            Bio = bio.Trim(),
            UserSettings = new UserSettings()
            {
                Status = UserStatus.RequiredActivate
            }
        };
    }

    public Either<Error, User> UpdateEmail(string newEmail)
    {
        if (Email.Equals(newEmail, StringComparison.OrdinalIgnoreCase))
            return Error.New("User already has this email");
        if (!new EmailAddressAttribute().IsValid(newEmail))
            return Error.New("Invalid email format");
        
        Email =  newEmail.Trim();
        UpdatedAt = DateTime.UtcNow;
        return this;
    }

    public Either<Error, User> UpdateUserData(string newUserName, string newBio)
    {
        if(string.IsNullOrEmpty(newUserName))
            return Error.New("Invalid user name");
        if (string.IsNullOrEmpty(newBio))
            return Error.New("Invalid bio");
        
        UserName = newUserName.Trim();
        Bio = newBio.Trim();
        UpdatedAt = DateTime.UtcNow;
        
        return this;
    }

    public Either<Error, User> AddFriend(User user)
    {
        if (Friends.Contains(user))
            return Error.New("Users already friends");
        
        Friends.Add(user);
        if (!user.Friends.Contains(this))
            user.Friends.Add(this);
        
        return this;
    }

    public Either<Error, User> RemoveFriend(User user)
    {
        if (!Friends.Contains(user))
            return Error.New("Users are not friends");
        
        Friends.Remove(user);
        if (user.Friends.Contains(this))
            user.Friends.Remove(this);
        
        return this;
    }

    public Either<Error, User> Seen()
    {
        LastSeenAt = DateTime.UtcNow;
        return this;
    }
    public Either<Error, User> Activate()
    {
        if (UserSettings.Status == UserStatus.Activated)
            return Error.New("User already active");
        
        UserSettings.Status = UserStatus.Activated;
        UserSettings.ActivatedAt = DateTime.UtcNow;
        UserSettings.BlockedAt = null;
        UserSettings.DeletedAt = null;
        return this;
    }

    public Either<Error, User> Block()
    {
        if (UserSettings.Status == UserStatus.Blocked)
            return Error.New("User already blocked");
        UserSettings.Status = UserStatus.Blocked;
        UserSettings.BlockedAt = DateTime.UtcNow;
        UserSettings.ActivatedAt = null;
        UserSettings.DeletedAt = null;
        return this;
    }

    public Either<Error, User> Delete()
    {
        if (UserSettings.Status == UserStatus.Deleted)
            return Error.New("User already deleted");
        UserSettings.Status = UserStatus.Deleted;
        UserSettings.DeletedAt = DateTime.UtcNow;
        UserSettings.BlockedAt = null;
        UserSettings.ActivatedAt = null;
        return this;
    }
}