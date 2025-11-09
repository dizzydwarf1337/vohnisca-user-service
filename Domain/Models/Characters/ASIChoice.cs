using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class ASIChoice
{
    public bool IsFeat { get; set; }
    public virtual Feature? FeatI{ get; set; }
    public Dictionary<AbilityScore, int>? AbilityIncreases { get; set; } 
}