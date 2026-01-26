using Domain.Models.Users.Enums;

namespace Domain.Models.Users;

public class UserSettings
{
    public DateTime? ActivatedAt { get; set; }
    
    public DateTime? BlockedAt { get; set; }
    
    public DateTime? DeletedAt { get; set; }

    public UserStatus Status { get; set; }
}