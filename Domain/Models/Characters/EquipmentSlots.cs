using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class EquipmentSlots
{
    public int? MinLevel { get; set; }
    
    public Dictionary<AbilityScore, int>? MinAbilityScores { get; set; }
    
    public List<WeaponProperty>? RequiredWeaponProficiencies { get; set; }
    public List<ArmorType>? RequiredArmorProficiencies { get; set; }
    
    public List<Guid>? RequiredFeatIds { get; set; }
    
    public List<Guid>? RequiredFeatureIds { get; set; }
    
    public bool RequiresSpellcasting { get; set; }
    
    public List<Guid>? RequiredRaceIds { get; set; }
}