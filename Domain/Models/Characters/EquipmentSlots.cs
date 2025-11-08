using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class EquipmentSlots
{
    public Guid? MainHandWeaponId { get; set; }
    public Guid? OffHandWeaponId { get; set; }
    
    public Guid? ArmorId { get; set; }
    public Guid? ShieldId { get; set; }
    
    public Dictionary<string, Guid> AccessorySlots { get; set; } = new();
    
    public List<Guid> AttunedItemIds { get; set; } = new();
    public int MaxAttunedItems { get; set; } = 3;
    
    public bool AttuneItem(Guid itemId)
    {
        if (AttunedItemIds.Contains(itemId)) return false;
        if (AttunedItemIds.Count >= MaxAttunedItems) return false;
        
        AttunedItemIds.Add(itemId);
        return true;
    }
    
    public bool UnattuneItem(Guid itemId)
    {
        return AttunedItemIds.Remove(itemId);
    }
    
    public bool IsAttuned(Guid itemId)
    {
        return AttunedItemIds.Contains(itemId);
    }
    
    public List<Guid> GetAllEquippedItems()
    {
        var items = new List<Guid>();
        
        if (MainHandWeaponId.HasValue) items.Add(MainHandWeaponId.Value);
        if (OffHandWeaponId.HasValue) items.Add(OffHandWeaponId.Value);
        if (ArmorId.HasValue) items.Add(ArmorId.Value);
        if (ShieldId.HasValue) items.Add(ShieldId.Value);
        
        items.AddRange(AccessorySlots.Values);
        
        return items;
    }
}