using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class SubRace
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Source { get; set; }
    
    public Dictionary<AbilityScore, int> AbilityScoreIncreases { get; set; } = new();
    
    public int? SpeedOverride { get; set; }
    
    public List<Language> BonusLanguages { get; set; } = new();
    
    public List<WeaponProperty> BonusWeaponProficiencies { get; set; } = new();
    public List<ToolProficiency> BonusToolProficiencies { get; set; } = new();
    
    public Dictionary<SenseType, int> BonusSenses { get; set; } = new();

    public ICollection<Feature> SubracialFeatures { get; set; } = new List<Feature>();
}