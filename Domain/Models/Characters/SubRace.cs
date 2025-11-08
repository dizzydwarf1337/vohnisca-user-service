using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class SubRace
{
    public Guid Id { get; set; }
    public Guid ParentRaceId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Source { get; set; }
    
    public Dictionary<AbilityScore, int> AbilityScoreIncreases { get; set; } = new();
    
    public List<Guid> SubracialFeatureIds { get; set; } = new();
    
    public int? SpeedOverride { get; set; }
    
    public List<string> BonusLanguages { get; set; } = new();
    
    public List<WeaponProperty> BonusWeaponProficiencies { get; set; } = new();
    public List<string> BonusToolProficiencies { get; set; } = new();
    
    public Dictionary<string, int> BonusSenses { get; set; } = new();
}