using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class ResourcePool
{
    public ResourceType Type { get; set; }
    public int MaxAmount { get; set; }
    public int CurrentAmount { get; set; }
    public UsageType RechargeType { get; set; }
    public string DisplayName { get; set; }
    public string Description { get; set; }
    
    public int? DiceSize { get; set; }
    
    public string? Formula { get; set; } 
}

public enum UsageType
{
    Unlimited,
    ShortRest,
    LongRest,
    Daily,
    Weekly,
    Recharge 
}