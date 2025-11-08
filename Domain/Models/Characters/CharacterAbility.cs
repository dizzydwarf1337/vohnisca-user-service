using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class CharacterAbility
{
    public AbilityScore Ability { get; init; }
    public int AbilityValue { get; private set; }

    public int AbilityModifier => (AbilityValue - 10) / 2;
    
    public bool HasSavingThrowProficiency { get; private set; }

    public IReadOnlyList<CharacterSkill> Skills => _skills.AsReadOnly();

    private readonly List<CharacterSkill> _skills = new();

    public CharacterAbility(AbilityScore ability, int abilityValue, bool hasSavingThrowProficiency = false)
    {
        Ability = ability;
        AbilityValue = abilityValue;
        HasSavingThrowProficiency = hasSavingThrowProficiency;
    }

    public void UpdateValue(int newValue)
    {
        AbilityValue = newValue;
    }
    
    public void SetSavingThrowProficiency(bool hasProficiency)
    {
        HasSavingThrowProficiency = hasProficiency;
    }
    
    public int GetSavingThrowModifier(int proficiencyBonus)
    {
        return AbilityModifier + (HasSavingThrowProficiency ? proficiencyBonus : 0);
    }

    internal void AddSkill(CharacterSkill skill)
    {
        _skills.Add(skill);
    }
}
