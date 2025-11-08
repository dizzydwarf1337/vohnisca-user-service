using Domain.Models.Characters.Enums;
using Domain.Models.Characters.Leveling;

namespace Domain.Models.Characters;

public class Class
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string IconUrl { get; set; }
    
    // Базовые параметры
    public int HitDieSize { get; set; }
    public AbilityScore PrimaryStat { get; set; }
    public List<AbilityScore> SavingThrowProficiencies { get; set; }
    
    // Начальные профы (1 уровень)
    public List<ArmorType> StartingArmorProficiencies { get; set; }
    public List<WeaponProperty> StartingWeaponProficiencies { get; set; }
    public List<string> StartingToolProficiencies { get; set; }
    
    // Выбор навыков
    public int SkillChoiceCount { get; set; }
    public List<Skill> AvailableSkills { get; set; }
    
    // Заклинательство
    public bool IsSpellcaster { get; set; }
    public SpellcastingType? SpellcastingType { get; set; }
    public AbilityScore? SpellcastingAbility { get; set; }
    public bool IsPreparedCaster { get; set; }
    
    // Подклассы
    public int SubclassLevel { get; set; }
    public List<Subclass> Subclasses { get; set; }
    
    // Прогрессия уровней (встроенная)
    public List<ClassLevelProgression> LevelProgressions { get; set; }
}