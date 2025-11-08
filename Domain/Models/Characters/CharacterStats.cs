using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class CharacterStats
{
    public int TotalLevel { get; set; }
    public int ProficiencyBonus => 2 + ((TotalLevel - 1) / 4);
    
    public int ExperiencePoints { get; set; }
    
    private readonly List<CharacterAbility> _abilities = new();
    public IReadOnlyList<CharacterAbility> Abilities => _abilities.AsReadOnly();
        
    public List<string> Languages { get; set; }
    public List<string> ToolProficiencies { get; set; }
    public List<WeaponProperty> WeaponProficiencies { get; set; }
    public List<ArmorType> ArmorProficiencies { get; set; }
    
    public int GetSkillModifier(Skill skill)
    {
        var ability = _abilities.FirstOrDefault(a => a.Skills.Any(s => s.Skill == skill));
        if (ability == null) return 0;
        
        var charSkill = ability.Skills.First(s => s.Skill == skill);
        return charSkill.GetModifier(ability.AbilityModifier, ProficiencyBonus);
    }
        
    public List<AbilityInfo> GetAllAbilities()
    {
        return _abilities.Select(ability => new AbilityInfo(
            ability.Ability,
            ability.AbilityValue,
            ability.AbilityModifier,
            ability.Skills.Select(s => new SkillInfo(
                s.Skill,
                s.Proficiency,
                s.GetModifier(ability.AbilityModifier, ProficiencyBonus)
            )).ToList()
        )).ToList();
    }
}

public record SkillInfo(Skill Skill, ProficiencyLevel Proficiency, int Modifier);

public record AbilityInfo(
    AbilityScore AbilityScore,
    int AbilityValue,
    int AbilityModifier,
    List<SkillInfo> Skills
);