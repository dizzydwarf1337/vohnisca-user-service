using Domain.Models.Characters.Enums;
using Domain.Models.Characters.Items;
using Domain.Models.Users;

namespace Domain.Models.Characters;

public class Character
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
        
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public string Name { get; set; }
    
    public Guid RaceId { get; set; }
    public virtual Race  Race { get; set; }
    public Guid? SubRaceId { get; set; }
    public virtual SubRace? SubRace { get; set; }
    public Guid BackgroundId { get; set; }
    public virtual Background Background { get; set; }
    
    public Guid? ConcentratingOnSpellId { get; set; }
    
    public Alignment Alignment { get; set; }
    
    public Size Size { get; set; }
    public int Age { get; set; }
    public int Height { get; set; } 
    public int Weight { get; set; } 
    public string Eyes { get; set; }
    public string Skin { get; set; }
    public string Hair { get; set; }
    public string AvatarUrl { get; set; }
    
    public List<CharacterClass> Classes { get; set; } = new();
    
    public CharacterStats Stats { get; set; } = new();
    
    public int MaxHitPoints { get; set; }
    public int CurrentHitPoints { get; set; }
    public int TemporaryHitPoints { get; set; }
    
    public int ArmorClass { get; set; }
    public int Speed { get; set; }
    public Dictionary<string, int> SpecialSpeeds { get; set; } = new();
    
    public Dictionary<int, int> HitDice { get; set; } = new();
    public Dictionary<int, int> HitDiceUsed { get; set; } = new();
    public int AttacksPerAction { get; set; } = 1;
    
    public int DeathSaveSuccesses { get; set; }
    public int DeathSaveFailures { get; set; }
    
    public bool HasInspiration { get; set; }
    
    public Dictionary<Guid, SpellcastingInfo> SpellcastingByClass { get; set; } = new();

    public List<CharacterItem> Inventory { get; set; } = new();
    public EquipmentSlots Equipment { get; set; } = new();
    public Currency Money { get; set; } = new();
    
    public List<DamageType> DamageResistances { get; set; } = new();
    public List<DamageType> DamageImmunities { get; set; } = new();
    public List<DamageType> DamageVulnerabilities { get; set; } = new();
    public List<Condition> ConditionImmunities { get; set; } = new();
    public List<Condition> CurrentConditions { get; set; } = new();
    
    public int ExhaustionLevel { get; set; }
    
    public Dictionary<ResourceType, ResourcePool> ClassResources { get; set; } = new();
    
    
    public string PersonalityTraits { get; set; }
    public string Ideals { get; set; }
    public string Bonds { get; set; }
    public string Flaws { get; set; }
    public string Backstory { get; set; }
    
    public List<string> AlliesAndOrganizations { get; set; } = new();
    public List<string> Enemies { get; set; } = new();
    
    public List<CharacterAction> AvailableActions { get; set; } = new();
    
    public virtual ICollection<Feature> SelectedFeatures { get; set; }
    public virtual User User { get; set; }
    
    public int GetTotalLevel()
    {
        return Classes.Sum(c => c.Level);
    }

    public int GetCarryingCapacity()
    {
        return Stats.GetCarryingCapacity();
    }
    
    public int GetInitiative()
    {
        return Stats.GetInitiative();
    }

    public int GetPassivePerception()
    {
        return Stats.GetPassivePerception();
    }
    
    public double GetCurrentWeight()
    {
        return Inventory.Sum(i => i.Item.Weight * i.Quantity);
    }
    
    public bool IsEncumbered()
    {
        return GetCurrentWeight() > GetCarryingCapacity();
    }
}