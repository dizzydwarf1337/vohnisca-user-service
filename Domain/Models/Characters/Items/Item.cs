using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters.Items;

public class Item
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Weight { get; set; }
    public int ValueInCopper { get; set; }
    public int Quantity { get; set; }
    public bool IsEquipped { get; set; }
    public ItemRarity Rarity { get; set; }
    public bool RequiresAttunement { get; set; }
    public bool IsAttuned { get; set; }
    public List<string> Properties { get; set; } = new();
    
    public ItemRequirements? Requirements { get; set; }
    
    public string Source { get; set; }
}