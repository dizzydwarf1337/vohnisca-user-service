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
    
    // Для dice (d6, d8, d10, d12)
    public int? DiceSize { get; set; }
    
    // Для ресурсов, которые зависят от других характеристик
    public string? Formula { get; set; } // Например: "Level * 5" для Lay on Hands, "Level + AbilityModifier" для Prepared Spells
}

public enum UsageType
{
    Unlimited,
    ShortRest,
    LongRest,
    Daily,
    Weekly,
    Recharge // Для вещей типа Dragon Breath
}