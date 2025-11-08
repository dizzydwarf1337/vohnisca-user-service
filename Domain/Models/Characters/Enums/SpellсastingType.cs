namespace Domain.Models.Characters.Enums;

public enum SpellcastingType
{
    Full,   // 9th level spells at 17 (Wizard, Cleric, etc)
    Half,   // 5th level spells at 17 (Paladin, Ranger)
    Third,  // 4th level spells at 19 (Eldritch Knight, Arcane Trickster)
    Pact,   // Warlock (short rest slots)
    None
}