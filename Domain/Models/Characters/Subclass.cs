using Domain.Models.Characters.Enums;
using Domain.Models.Characters.Leveling;

namespace Domain.Models.Characters;

public class Subclass
{
    public Guid Id { get; set; }
    public Guid ParentClassId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string IconUrl { get; set; }
    
    // Дополнительные профы (если есть)
    public List<ArmorType>? BonusArmorProficiencies { get; set; }
    public List<WeaponProperty>? BonusWeaponProficiencies { get; set; }
    
    // Прогрессия подкласса (встроенная)
    public List<ClassLevelProgression> LevelProgressions { get; set; }
    
    // Бонусные заклинания
    public Dictionary<int, List<Guid>>? BonusSpellsByLevel { get; set; }
}