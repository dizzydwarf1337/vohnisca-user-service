namespace Domain.Models.Characters.Leveling;

public class ClassLevelProgression
{
    public int Level { get; set; }
    
    public List<ResourceProgression> ResourceProgressions { get; set; }
    
    public List<Guid> FeatureIds { get; set; }
    
    public List<FeatureChoiceProgression> FeatureChoices { get; set; }
    
    public bool GrantsASI { get; set; }
}