using Domain.Models.Characters.Items;

namespace Domain.Models.Characters;

public class CharacterInventory
{
    public List<CharacterItem> Items { get; set; } = new();
    public EquipmentSlots Equipment { get; set; } = new();
    public Currency Money { get; set; } = new();

    public double TotalWeight => Items.Sum(i => i.Item.Weight * i.Quantity);
}