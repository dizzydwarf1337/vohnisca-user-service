using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class FeatureRequirements
{
    public int? MinLevel { get; set; }
    public List<Guid>? RequiredFeatureIds { get; set; } 
    public Dictionary<AbilityScore, int>? MinAbilityScores { get; set; }
    public List<Guid>? RequiredClassIds { get; set; }
}