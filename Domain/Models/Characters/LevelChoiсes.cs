namespace Domain.Models.Characters;

public class LevelChoices
{
    public Dictionary<string, List<Guid>> FeatureChoices { get; set; } = new();
    
    public ASIChoice? ASIChoice { get; set; }
    
    public int HitPointsGained { get; set; }
}