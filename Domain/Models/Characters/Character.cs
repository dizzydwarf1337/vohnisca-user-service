using Domain.Models.Characters.Enums;
using Domain.Models.Characters.Items;

namespace Domain.Models.Characters;

public class Character
{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Race Race { get; set; }
        public SubRace SubRace { get; set; }
        public List<ClassLevel> Classes { get; set; }
        public Alignment Alignment { get; set; }
        public Background Background { get; set; }
        public Size Size { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Eyes { get; set; }
        public string Skin { get; set; }
        public string Hair { get; set; }
        
        public CharacterStats Stats { get; set; }
        
        public int MaxHitPoints { get; set; }
        public int CurrentHitPoints { get; set; }
        public int TemporaryHitPoints { get; set; }
        public int ArmorClass { get; set; }
        public int Speed { get; set; }
        public Dictionary<string, int> SpecialSpeeds { get; set; } 
        
        public Dictionary<int, int> HitDice { get; set; } 
        public Dictionary<int, int> HitDiceUsed { get; set; }
        
        public int DeathSaveSuccesses { get; set; }
        public int DeathSaveFailures { get; set; }
        
        public bool HasInspiration { get; set; }
        
        public List<Feature> Features { get; set; }
        
        public Dictionary<Class, SpellcastingInfo> SpellcastingClasses { get; set; }
        public List<Spell> KnownSpells { get; set; }
        public List<Spell> PreparedSpells { get; set; }
        public Dictionary<int, int> SpellSlots { get; set; }

        public List<Item> Inventory { get; set; }
        public List<Weapon> Weapons { get; set; }
        public Armor EquippedArmor { get; set; }
        public Armor EquippedShield { get; set; }
        public Currency Money { get; set; }
        public int CarryingCapacity { get; set; }

        public List<DamageType> DamageResistances { get; set; }
        public List<DamageType> DamageImmunities { get; set; }
        public List<DamageType> DamageVulnerabilities { get; set; }
        public List<Condition> ConditionImmunities { get; set; }
        public List<Condition> CurrentConditions { get; set; }

        public int ExhaustionLevel { get; set; }

        public Dictionary<string, ResourcePool> ClassResources { get; set; }

        public string PersonalityTraits { get; set; }
        public string Ideals { get; set; }
        public string Bonds { get; set; }
        public string Flaws { get; set; }
        public string Backstory { get; set; }

        public List<string> AlliesAndOrganizations { get; set; }
       
        public List<string> Enemies { get; set; }

        public List<string> OtherProficienciesAndLanguages { get; set; }

        public Dictionary<string, int> Senses { get; set; } 
}


