using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class SubRace
{
    [Key]
    public Guid Id { get; set; }
    
    public Guid RaceId { get; set; }
    
    public string Name { get; set; }
    public string Description { get; set; }
    public string Source { get; set; }
    
    public Dictionary<AbilityScore, int> AbilityScoreIncreases { get; set; } = new();
    
    public List<Guid> SubracialFeatureIds { get; set; } = new();
    
    public int? SpeedOverride { get; set; }
    
    public List<Language> BonusLanguages { get; set; } = new();
    
    public List<WeaponProperty> BonusWeaponProficiencies { get; set; } = new();
    public List<ToolProficiency> BonusToolProficiencies { get; set; } = new();
    
    public Dictionary<SenseType, int> BonusSenses { get; set; } = new();
    
    [ForeignKey(nameof(RaceId))]
    public virtual Race Race { get; set; }
}