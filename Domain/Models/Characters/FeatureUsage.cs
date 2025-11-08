namespace Domain.Models.Characters;

public class FeatureUsage
{
    public UsageType Type { get; set; }
    public string? MaxUsesFormula { get; set; } // "3", "ProficiencyBonus", "Level / 3"
    public int CurrentUses { get; set; }
    public string? RechargeCondition { get; set; } // "1d6 (5-6)" для Dragon Breath
}