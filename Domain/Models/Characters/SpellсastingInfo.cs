using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class SpellcastingInfo
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
    
    public bool CanCastRituals { get; set; }
    
    
    public int GetAvailableSlots(int level)
    {
        if (!TotalSlots.TryGetValue(level, out var total)) return 0;
        var used = UsedSlots.TryGetValue(level, out var slot) ? slot : 0;
        return total - used;
    }
    
    public bool UseSpellSlot(int level)
    {
        if (GetAvailableSlots(level) <= 0) return false;
        
        UsedSlots.TryAdd(level, 0);
        
        UsedSlots[level]++;
        return true;
    }
    
    public void RestoreAllSlots()
    {
        UsedSlots.Clear();
    }
    
    public bool PrepareSpell(Guid spellId)
    {
        if (PreparedSpellIds.Contains(spellId)) return false;
        PreparedSpellIds.Add(spellId);
        return true;
    }
    
    public bool UnprepareSpell(Guid spellId)
    {
        return PreparedSpellIds.Remove(spellId);
    }
}