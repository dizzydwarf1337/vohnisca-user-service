using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class SpellcastingInfo
{
    public Guid ClassId { get; set; }
    public virtual Class Class { get; set; }
    
    public SpellcastingType Type { get; set; }
    public AbilityScore SpellcastingAbility { get; set; }
    
    public int SpellSaveDC { get; set; }
    public int SpellAttackBonus { get; set; }
    
    public Dictionary<int, int> TotalSlots { get; set; } = new();
    public Dictionary<int, int> UsedSlots { get; set; } = new();
    
    public List<Spell> KnownSpells { get; set; } = new();
    
    public List<Spell> PreparedSpells { get; set; } = new();
    
    public int MaxPreparedSpells { get; set; }

    public ICollection<Spell> AlwaysPreparedSpells { get; set; } = new HashSet<Spell>();
    
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
    
    public bool PrepareSpell(Spell spell)
    {
        if (PreparedSpells.Any(x => x.Id == spell.Id)) return false;
        PreparedSpells.Add(spell);
        return true;
    }
    
    public bool UnprepareSpell(Spell spell)
    {
        return PreparedSpells.Remove(spell);
    }
}