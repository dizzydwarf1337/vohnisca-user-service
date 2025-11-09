using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters.Items;

public class ItemRequirements
{
    public int? MinLevel { get; set; }
    
    public Dictionary<AbilityScore, int>? MinAbilityScores { get; set; }
    
    public List<WeaponProperty>? RequiredWeaponProficiencies { get; set; }
    public List<ArmorType>? RequiredArmorProficiencies { get; set; }
    
    public bool RequiresSpellcasting { get; set; }
    
    public virtual ICollection<Race>? RequiredRaceIds { get; set; }
    public virtual ICollection<Class>? RequiredClassIds { get; set; }
    public virtual ICollection<Feature>? RequiredFeatureId { get; set; }
}