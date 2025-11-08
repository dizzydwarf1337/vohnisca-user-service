using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters.Leveling;

public class ResourceProgression
{
    public ResourceType ResourceType { get; set; }
    public ResourceProgressionAction Action { get; set; }
    
    // Значение (может быть число или формула)
    public string Value { get; set; } // "2", "Level", "Level * 5", "ProficiencyBonus + 2"
    
    // Для dice resources
    public int? DiceSize { get; set; }
    
    // Настройки ресурса (если создается впервые)
    public UsageType? RechargeType { get; set; }
    public string? DisplayName { get; set; }
    public string? Description { get; set; }
}