using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters.Items;

public class Item
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Source { get; set; }
    public double Weight { get; set; }
    public int ValueInCopper { get; set; }
    public ItemRarity Rarity { get; set; }
    public List<ItemProperty> Properties { get; set; } = new();
    public ItemRequirements? Requirements { get; set; }
    
    public virtual ICollection<Background> Backgrounds { get; set; } = new List<Background>();
}