using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class CharacterCombat
{
    public int MaxHitPoints { get; set; }
    public int CurrentHitPoints { get; set; }
    public int TemporaryHitPoints { get; set; }

    public int ArmorClass { get; set; }
    public int Speed { get; set; }
    public Dictionary<string, int> SpecialSpeeds { get; set; } = new();

    public int DeathSaveSuccesses { get; set; }
    public int DeathSaveFailures { get; set; }

    public int AttacksPerAction { get; set; } = 1;
    public bool HasInspiration { get; set; }

    public List<DamageType> DamageResistances { get; set; } = new();
    public List<DamageType> DamageImmunities { get; set; } = new();
    public List<DamageType> DamageVulnerabilities { get; set; } = new();

    public List<Condition> ConditionImmunities { get; set; } = new();
    public List<Condition> CurrentConditions { get; set; } = new();

    public int ExhaustionLevel { get; set; }
}
