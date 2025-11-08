using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class Race
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Source { get; set; }
    
    public Size Size { get; set; }
    public int BaseSpeed { get; set; }
    public List<string> Languages { get; set; } = new();
    
    public Dictionary<AbilityScore, int> AbilityScoreIncreases { get; set; } = new();
    
    public List<Guid> RacialFeatureIds { get; set; } = new();
    
    public Dictionary<string, int> Senses { get; set; } = new();
    
    public List<SubRace> SubRaces { get; set; } = new();
}