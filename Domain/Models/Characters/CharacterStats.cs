using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class CharacterStats
{
    public int TotalLevel { get; set; }
    public int ProficiencyBonus => 2 + ((TotalLevel - 1) / 4);
    public int ExperiencePoints { get; set; }
    
    public List<CharacterAbility> Abilities = new();
    public List<Language> Languages { get; set; } = new();
    public List<ToolProficiency> ToolProficiencies { get; set; } = new();
    public List<WeaponProperty> WeaponProficiencies { get; set; } = new();
    public List<ArmorType> ArmorProficiencies { get; set; } = new();
    
    public Dictionary<SenseType, int> Senses { get; set; } = new();
    
    public int GetSkillModifier(Skill skill)
    {
        var ability = Abilities.FirstOrDefault(a => a.Skills.Any(s => s.Skill == skill));
        if (ability == null) return 0;
        
        var charSkill = ability.Skills.First(s => s.Skill == skill);
        return charSkill.GetModifier(ability.AbilityModifier, ProficiencyBonus);
    }
    
    public int GetAbilityModifier(AbilityScore ability)
    {
        var abilityData = Abilities.FirstOrDefault(a => a.Ability == ability);
        return abilityData?.AbilityModifier ?? 0;
    }
    
    public int GetAbilityValue(AbilityScore ability)
    {
        var abilityData = Abilities.FirstOrDefault(a => a.Ability == ability);
        return abilityData?.AbilityValue ?? 10;
    }
    
    public int GetSavingThrowModifier(AbilityScore ability)
    {
        var abilityData = Abilities.FirstOrDefault(a => a.Ability == ability);
        return abilityData?.GetSavingThrowModifier(ProficiencyBonus) ?? 0;
    }
    
    public int GetCarryingCapacity()
    {
        return GetAbilityValue(AbilityScore.Strength) * 15;
    }
    
    public int GetInitiative()
    {
        return GetAbilityModifier(AbilityScore.Dexterity);
    }
    
    public int GetPassivePerception()
    {
        return 10 + GetSkillModifier(Skill.Perception);
    }
    
    public int GetPassiveInvestigation()
    {
        return 10 + GetSkillModifier(Skill.Investigation);
    }
    
    public int GetPassiveInsight()
    {
        return 10 + GetSkillModifier(Skill.Insight);
    }
    
    public List<AbilityInfo> GetAllAbilities()
    {
        return Abilities.Select(ability => new AbilityInfo(
            ability.Ability,
            ability.AbilityValue,
            ability.AbilityModifier,
            ability.HasSavingThrowProficiency, 
            ability.GetSavingThrowModifier(ProficiencyBonus), 
            ability.Skills.Select(s => new SkillInfo(
                s.Skill,
                s.Proficiency,
                s.GetModifier(ability.AbilityModifier, ProficiencyBonus)
            )).ToList()
        )).ToList();
    }
    
    public void InitializeAbilities()
    {
        if (Abilities.Count > 0) return;
        
        foreach (AbilityScore abilityScore in Enum.GetValues(typeof(AbilityScore)))
        {
            var ability = new CharacterAbility(abilityScore, 10);
            
            var relatedSkills = GetRelatedSkills(abilityScore);
            foreach (var skill in relatedSkills)
            {
                ability.AddSkill(new CharacterSkill(skill));
            }
            
            Abilities.Add(ability);
        }
    }
    
    public void SetAbilityValue(AbilityScore ability, int value)
    {
        var abilityData = Abilities.FirstOrDefault(a => a.Ability == ability);
        abilityData?.UpdateValue(value);
    }
    
    public void SetSkillProficiency(Skill skill, ProficiencyLevel proficiency)
    {
        var ability = Abilities.FirstOrDefault(a => a.Skills.Any(s => s.Skill == skill));
        var charSkill = ability?.Skills.FirstOrDefault(s => s.Skill == skill);
        charSkill?.SetProficiency(proficiency);
    }
    
    public void SetSavingThrowProficiency(AbilityScore ability, bool hasProficiency)
    {
        var abilityData = Abilities.FirstOrDefault(a => a.Ability == ability);
        abilityData?.SetSavingThrowProficiency(hasProficiency);
    }
    
    private static List<Skill> GetRelatedSkills(AbilityScore ability)
    {
        return ability switch
        {
            AbilityScore.Strength => new() { Skill.Athletics },
            AbilityScore.Dexterity => new() { Skill.Acrobatics, Skill.SleightOfHand, Skill.Stealth },
            AbilityScore.Constitution => new(),
            AbilityScore.Intelligence => new() { Skill.Arcana, Skill.History, Skill.Investigation, Skill.Nature, Skill.Religion },
            AbilityScore.Wisdom => new() { Skill.AnimalHandling, Skill.Insight, Skill.Medicine, Skill.Perception, Skill.Survival },
            AbilityScore.Charisma => new() { Skill.Deception, Skill.Intimidation, Skill.Performance, Skill.Persuasion },
            _ => new()
        };
    }
}

public record SkillInfo(Skill Skill, ProficiencyLevel Proficiency, int Modifier);

public record AbilityInfo(
    AbilityScore AbilityScore,
    int AbilityValue,
    int AbilityModifier,
    bool HasSavingThrowProficiency,
    int SavingThrowModifier,
    List<SkillInfo> Skills
);