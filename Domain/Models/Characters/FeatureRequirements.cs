using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class FeatureRequirements
{
    public int? MinLevel { get; set; }
    
    public Dictionary<AbilityScore, int>? MinAbilityScores { get; set; }
    
    public List<WeaponProperty>? RequiredWeaponProficiencies { get; set; }
    public List<ArmorType>? RequiredArmorProficiencies { get; set; }
    
    public ICollection<Feature>? RequiredFeatures { get; set; }
    
    public bool RequiresSpellcasting { get; set; }
    
    public ICollection<Race>? RequiredRaces { get; set; }
}