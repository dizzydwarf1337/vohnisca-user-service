namespace Domain.Models.Characters.Items;

public class CharacterItem
{
    public Guid ItemId { get; set; }
    public virtual Item Item { get; set; }  
    public int Quantity { get; set; }       
    public bool IsEquipped { get; set; }    
    public bool IsAttuned { get; set; }     
}