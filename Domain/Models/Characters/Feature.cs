using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class Feature
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Source { get; set; } 
    
    public FeatureSource SourceType { get; set; }
    
    public FeatureType Type { get; set; }
    
    public FeatureRequirements? Requirements { get; set; }
    
    public List<FeatureEffect> Effects { get; set; } = new();
    
    public FeatureUsage? Usage { get; set; }
    
    public bool GrantsHalfASI { get; set; }
    
    public List<AbilityScore> AllowedAbilitiesForHalfASI { get; set; } = new();
    
    public Dictionary<AbilityScore, int>? AbilityIncreases { get; set; }
    
    public bool IsSelectableAsFeat { get; set; }
    
    public virtual ICollection<Character> Characters { get; set; } = new List<Character>();
    public virtual ICollection<Race> Races { get; set; } = new List<Race>();
}