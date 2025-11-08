using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class FeatureUsage
{
    public UsageType Type { get; set; }
    public string? MaxUsesFormula { get; set; } 
    public int CurrentUses { get; set; }
    public string? RechargeCondition { get; set; } 
}