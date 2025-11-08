using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class Spell
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Source { get; set; } 
    public int Level { get; set; }
    public SpellSchool School { get; set; }
    public string CastingTime { get; set; }
    public string Range { get; set; }
    public bool HasVerbalComponent { get; set; }
    public bool HasSomaticComponent { get; set; }
    public bool HasMaterialComponent { get; set; }
    public string MaterialComponents { get; set; }
    public string Duration { get; set; }
    public string Description { get; set; }
    public string AtHigherLevels { get; set; }
    public bool IsRitual { get; set; }
    public bool IsConcentration { get; set; }
    
    public List<Guid> AvailableForClassIds { get; set; } = new();
}