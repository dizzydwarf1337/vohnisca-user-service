using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class CharacterSkill
{
    public Skill Skill { get; init; }
    public ProficiencyLevel Proficiency { get; private set; }
    
    public CharacterSkill(Skill skill, ProficiencyLevel proficiency = ProficiencyLevel.None)
    {
        Skill = skill;
        Proficiency = proficiency;
    }
    
    public int GetModifier(int abilityModifier, int proficiencyBonus)
    {
        int profBonus = Proficiency switch
        {
            ProficiencyLevel.HalfProficient => proficiencyBonus / 2,
            ProficiencyLevel.Proficient => proficiencyBonus,
            ProficiencyLevel.Expertise => proficiencyBonus * 2,
            _ => 0
        };
        return abilityModifier + profBonus;
    }
    
    public void SetProficiency(ProficiencyLevel level) => Proficiency = level;
}