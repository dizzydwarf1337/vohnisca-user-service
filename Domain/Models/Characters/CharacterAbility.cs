using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class CharacterAbility
{
    public AbilityScore Ability { get; init; }
    public int AbilityValue { get; private set; }
    
    public int AbilityModifier => (AbilityValue - 10) / 2;

    public IReadOnlyList<CharacterSkill> Skills => _skills.AsReadOnly();
    
    private readonly List<CharacterSkill> _skills = new();

    public CharacterAbility(AbilityScore ability, int abilityValue)
    {
        Ability = ability;
        AbilityValue = abilityValue;
    }

    public void UpdateValue(int newValue)
    {
        AbilityValue = newValue;
    }

    internal void AddSkill(CharacterSkill skill)
    {
        _skills.Add(skill);
    }

}