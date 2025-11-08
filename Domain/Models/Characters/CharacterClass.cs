namespace Domain.Models.Characters;

public class CharacterClass
{
    public Guid Id { get; set; }
    public Guid CharacterId { get; set; }
    
    public Guid ClassDefinitionId { get; set; }
    
    public int Level { get; set; }
    
    public Guid? SubclassId { get; set; }
    
    public Dictionary<int, LevelChoices> ChoicesByLevel { get; set; } = new();
    
    public Dictionary<int, int> HitPointsByLevel { get; set; } = new();
}