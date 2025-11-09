using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class FeatureEffect
{
    public Guid Id { get; set; }
    public EffectType Type { get; set; }
    public EffectTarget Target { get; set; }
    public string? CustomTarget { get; set; }
    public string Value { get; set; }
    public Dictionary<string, string>? Conditions { get; set; } 
}