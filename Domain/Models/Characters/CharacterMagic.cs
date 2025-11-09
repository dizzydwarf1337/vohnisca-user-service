using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class CharacterMagic
{
    public Guid? ConcentratingOnSpellId { get; set; }
    public virtual Spell? ConcentratingOnSpell { get; set; }
    public Dictionary<Guid, SpellcastingInfo> SpellcastingByClass { get; set; } = new();
    public Dictionary<ResourceType, ResourcePool> ClassResources { get; set; } = new();
}