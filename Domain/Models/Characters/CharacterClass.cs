
namespace Domain.Models.Characters;

public class CharacterClass
{
    public Class Class { get; set; }
    public Subclass? Subclass{ get; set; }
    
    public int Level { get; set; }
    public Dictionary<int, LevelChoices> ChoicesByLevel { get; set; } = new();
    public Dictionary<int, int> HitPointsByLevel { get; set; } = new();
    

}