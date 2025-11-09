using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class CharacterAction
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public FeatureType ActionType { get; set; } 
    
    public ActionSource Source { get; set; }
    public Guid SourceId { get; set; } 
    
    public int? AttackBonus { get; set; }
    public DiceType? DamageDiceType { get; set; }
    public int? DamageDiceCount { get; set; }
    public DamageType? DamageType { get; set; }
    public int? DamageBonus { get; set; }
    
    public int? SpellLevel { get; set; }
    public string Range { get; set; }
    
    public UsageType UsageType { get; set; }
    public int? UsesRemaining { get; set; }
}