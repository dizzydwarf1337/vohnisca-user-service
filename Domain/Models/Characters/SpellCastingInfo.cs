using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class SpellCastingInfo
{
    public Guid ClassDefinitionId { get; set; } 
    public SpellcastingType Type { get; set; } 
    public AbilityScore SpellcastingAbility { get; set; }
    
    public int SpellSaveDC { get; set; } 
    public int SpellAttackBonus { get; set; } 

    public Dictionary<int, int> TotalSlots { get; set; } = new(); 
    public Dictionary<int, int> UsedSlots { get; set; } = new();
    

    public List<Guid> KnownSpellIds { get; set; } = new();
    
 
    public List<Guid> PreparedSpellIds { get; set; } = new();

    public List<Guid> AlwaysPreparedSpellIds { get; set; } = new();

    public int KnownCantrips { get; set; }
}