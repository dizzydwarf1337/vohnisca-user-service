using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class CharacterMagic
{
    public Spell? ConcentratingOnSpellId { get; set; }
    public Dictionary<Guid, SpellcastingInfo> SpellcastingByClass { get; set; } = new();
    public Dictionary<ResourceType, ResourcePool> ClassResources { get; set; } = new();
}