using Domain.Models.Characters.Enums;
using Domain.Models.Characters.Items;

namespace Domain.Models.Characters;

public class Character
{
        public Guid Id { get; set; }
        // Базовая информация
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

        // Характеристики
        public CharacterStats Stats { get; set; }
        
        // Боевые характеристики
        public int MaxHitPoints { get; set; }
        public int CurrentHitPoints { get; set; }
        public int TemporaryHitPoints { get; set; }
        public int ArmorClass { get; set; }
        public int Speed { get; set; }
        public Dictionary<string, int> SpecialSpeeds { get; set; } // Climbing, Flying, Swimming
        
        // Кости хитов
        public Dictionary<int, int> HitDice { get; set; } // Тип кости -> количество
        public Dictionary<int, int> HitDiceUsed { get; set; }

        // Спасброски

        // Смерть и стабилизация
        public int DeathSaveSuccesses { get; set; }
        public int DeathSaveFailures { get; set; }
        
        // Вдохновение
        public bool HasInspiration { get; set; }

        // Особенности и умения
        public List<Feature> Features { get; set; }

        // Магия
        public Dictionary<Class, SpellcastingInfo> SpellcastingClasses { get; set; }
        public List<Spell> KnownSpells { get; set; }
        public List<Spell> PreparedSpells { get; set; }
        public Dictionary<int, int> SpellSlots { get; set; }
        
        // Снаряжение и деньги
        public List<Item> Inventory { get; set; }
        public List<Weapon> Weapons { get; set; }
        public Armor EquippedArmor { get; set; }
        public Armor EquippedShield { get; set; }
        public Currency Money { get; set; }
        public int CarryingCapacity { get; set; }

        // Сопротивления и иммунитеты
        public List<DamageType> DamageResistances { get; set; }
        public List<DamageType> DamageImmunities { get; set; }
        public List<DamageType> DamageVulnerabilities { get; set; }
        public List<Condition> ConditionImmunities { get; set; }
        public List<Condition> CurrentConditions { get; set; }

        // Состояние отдыха и восстановления
        public int ExhaustionLevel { get; set; }

        // Особые ресурсы классов
        public Dictionary<string, ResourcePool> ClassResources { get; set; }

        // Личность персонажа
        public string PersonalityTraits { get; set; }
        public string Ideals { get; set; }
        public string Bonds { get; set; }
        public string Flaws { get; set; }
        public string Backstory { get; set; }

        // Союзники и организации
        public List<string> AlliesAndOrganizations { get; set; }
        
        // Враги и соперники
        public List<string> Enemies { get; set; }

        // Дополнительные способности
        public List<string> OtherProficienciesAndLanguages { get; set; }

        // Сенсы
        public Dictionary<string, int> Senses { get; set; } // Darkvision, Blindsight, etc.
        
        
}


