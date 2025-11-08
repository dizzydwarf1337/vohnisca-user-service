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
}