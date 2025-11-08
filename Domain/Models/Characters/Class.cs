using Domain.Models.Characters.Enums;
using Domain.Models.Characters.Leveling;

namespace Domain.Models.Characters;

public class Class
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Source { get; set; } 
    public string IconUrl { get; set; }
    
    public DiceType HitDie { get; set; }
    public AbilityScore PrimaryStat { get; set; }
    public List<AbilityScore> SavingThrowProficiencies { get; set; } = new();

    public List<ArmorType> StartingArmorProficiencies { get; set; } = new();
    public List<WeaponProperty> StartingWeaponProficiencies { get; set; } = new();
    public List<ToolProficiency> StartingToolProficiencies { get; set; } = new();
    
    public int SkillChoiceCount { get; set; }
    public List<Skill> AvailableSkills { get; set; } = new();
    
    public bool IsSpellcaster { get; set; }
    public SpellcastingType? SpellcastingType { get; set; }
    public AbilityScore? SpellcastingAbility { get; set; }
    public bool IsPreparedCaster { get; set; }
    
    public int SubclassLevel { get; set; }
    public List<Subclass> Subclasses { get; set; } = new();
    
    public List<ClassLevelProgression> LevelProgressions { get; set; } = new();
    
    public Dictionary<AbilityScore, int>? MulticlassPrerequisites { get; set; }
    
    public List<ArmorType>? MulticlassArmorProficiencies { get; set; }
    public List<WeaponProperty>? MulticlassWeaponProficiencies { get; set; }
    public List<ToolProficiency>? MulticlassToolProficiencies { get; set; }
    public int? MulticlassSkillChoiceCount { get; set; }
}