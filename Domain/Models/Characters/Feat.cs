using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class Feat
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Source { get; set; }
    
    public FeatureRequirements? Requirements { get; set; }
    
    public List<FeatureEffect> Effects { get; set; } = new();
    
    public bool GrantsHalfASI { get; set; }
    public List<AbilityScore> AllowedAbilitiesForASI { get; set; } = new();
    
    public Dictionary<AbilityScore, int>? AbilityIncreases { get; set; }
}