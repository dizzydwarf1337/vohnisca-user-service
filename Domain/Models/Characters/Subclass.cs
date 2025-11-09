using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Characters.Enums;
using Domain.Models.Characters.Leveling;

namespace Domain.Models.Characters;

public class Subclass
{
    [Key]
    public Guid Id { get; set; }
    public Guid ClassId { get; set; }
    
    public string Name { get; set; }
    public string Description { get; set; }
    public string Source { get; set; }
    public string IconUrl { get; set; }
    
    public List<ArmorType>? BonusArmorProficiencies { get; set; }
    public List<WeaponProperty>? BonusWeaponProficiencies { get; set; }
    public List<ToolProficiency>? BonusToolProficiencies { get; set; }
    
    public List<ClassLevelProgression> LevelProgressions { get; set; } = new();
    
    public Dictionary<int, List<Guid>>? BonusSpellsByLevel { get; set; }
    
    [ForeignKey(nameof(ClassId))]
    public virtual Class Class { get; set; }
}