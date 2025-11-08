namespace Domain.Models.Characters.Enums;

public enum ResourceType
{
    // Общие для заклинателей
    KnownCantrips,
    SpellSlot1,
    SpellSlot2,
    SpellSlot3,
    SpellSlot4,
    SpellSlot5,
    SpellSlot6,
    SpellSlot7,
    SpellSlot8,
    SpellSlot9,
    KnownSpells,
    PreparedSpells,
    
    // Barbarian
    Rage,
    RageDamage,
    
    // Bard
    BardicInspiration,
    BardicInspirationDie,
    MagicalSecrets,
    
    // Cleric
    ChannelDivinity,
    DestroyUndeadCR,
    DivineIntervention,
    
    // Druid
    WildShape,
    WildShapeMaxCR,
    
    // Fighter
    ActionSurge,
    SecondWind,
    Indomitable,
    
    // Monk
    KiPoints,
    MartialArtsDie,
    UnarmoredMovement,
    
    // Paladin
    LayOnHands,
    DivineSmite,
    
    // Ranger
    FavoredEnemies,
    NaturalExplorer,
    
    // Rogue
    SneakAttackDice,
    
    // Sorcerer
    SorceryPoints,
    MetamagicOptions,
    
    // Warlock
    WarlockSpellSlot,
    WarlockSlotLevel,
    EldritchInvocations,
    MysticArcanum,
    
    // Wizard
    ArcaneRecovery,
    
    // Artificer
    InfusionsKnown,
    InfusedItems,
    
    // Специальные
    SuperiorityDice, // Battle Master
    SuperiorityDiceSize,
    Maneuvers,
    
    ExtraAttack, // Количество дополнительных атак
    
    // Подклассовые ресурсы
    SpellPoints, // Некоторые хоумбрю
    FavoredFoe, // Ranger Tasha's
    PrimalAwareness, // Ranger
    
    // Monastic Traditions
    OpenHandTechnique,
    ShadowArts,
    FourElements,
    
    // Warlock Pact Boons
    PactSlots,
    
    // Misc
    ASI, // Ability Score Improvements
    FeatSlots,
}