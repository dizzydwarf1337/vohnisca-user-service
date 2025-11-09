using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters.Leveling;

public class ResourceProgression
{
    public ResourceType ResourceType { get; set; }
    public ResourceProgressionAction Action { get; set; }
    
    public int? FixedValue { get; set; }
    
    public DiceType? DiceType { get; set; }
    public int? DiceCount { get; set; }
    
    public string? Formula { get; set; }
    
    public UsageType? RechargeType { get; set; }
    public string? DisplayName { get; set; }
    public string? Description { get; set; }
}