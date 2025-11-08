using Domain.Models.Characters.Enums;
using Domain.Models.Characters.Items;

namespace Domain.Models.Characters;

public class Character
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    
    public Guid RaceId { get; set; }
    public Guid? SubRaceId { get; set; }
    public Guid BackgroundId { get; set; }
    
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
    
    public CharacterStats Stats { get; set; }
    
    public int MaxHitPoints { get; set; }
    public int CurrentHitPoints { get; set; }
    public int TemporaryHitPoints { get; set; }
    
    public int ArmorClass { get; set; } 
    public int Speed { get; set; } 
    public Dictionary<string, int> SpecialSpeeds { get; set; } = new();
    
    public Dictionary<int, int> HitDice { get; set; } = new();
    public Dictionary<int, int> HitDiceUsed { get; set; } = new();
    
    public int DeathSaveSuccesses { get; set; }
    public int DeathSaveFailures { get; set; }
    
    public bool HasInspiration { get; set; }
    
    public Dictionary<Guid, SpellCastingInfo> SpellcastingByClass { get; set; } = new();
    
    public List<Item> Inventory { get; set; } = new();
    public EquipmentSlots Equipment { get; set; } = new();
    public Currency Money { get; set; } = new();
    public int CarryingCapacity { get; set; } 
    
    public List<DamageType> DamageResistances { get; set; } = new();
    public List<DamageType> DamageImmunities { get; set; } = new();
    public List<DamageType> DamageVulnerabilities { get; set; } = new();
    public List<Condition> ConditionImmunities { get; set; } = new();
    public List<Condition> CurrentConditions { get; set; } = new();
    
    public int ExhaustionLevel { get; set; }
    
    public Dictionary<ResourceType, ResourcePool> ClassResources { get; set; } = new();

    public List<Guid> SelectedFeatIds { get; set; } = new();
    

    public Dictionary<string, int> Senses { get; set; } = new();
    
    public string PersonalityTraits { get; set; }
    public string Ideals { get; set; }
    public string Bonds { get; set; }
    public string Flaws { get; set; }
    public string Backstory { get; set; }
    
    public List<string> AlliesAndOrganizations { get; set; } = new();
    public List<string> Enemies { get; set; } = new();
    
    public List<string> Languages { get; set; } = new();
    
    public List<string> ToolProficiencies { get; set; } = new();
    public List<WeaponProperty> WeaponProficiencies { get; set; } = new();
    public List<ArmorType> ArmorProficiencies { get; set; } = new();
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}