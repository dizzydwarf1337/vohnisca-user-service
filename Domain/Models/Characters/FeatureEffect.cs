using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class FeatureEffect
{
    public Guid Id { get; set; }
    public EffectType Type { get; set; }
    public string Target { get; set; } // "AC", "Speed", "Attack", "Damage", "SavingThrow.Strength"
    public string Value { get; set; } // "2", "+ProficiencyBonus", "1d6", "AbilityModifier.Wisdom"
    public Dictionary<string, string>? Conditions { get; set; } // "When": "Raging", "Against": "Undead"
}