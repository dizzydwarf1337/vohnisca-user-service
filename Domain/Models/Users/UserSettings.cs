using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Users.Enums;
using LanguageExt;
using LanguageExt.Common;

namespace Domain.Models.Users;

public class UserSettings
{
    public DateTime? ActivatedAt { get; set; }
    
    public DateTime? BlockedAt { get; set; }
    
    public DateTime? DeletedAt { get; set; }

    public UserStatus Status { get; set; }
}