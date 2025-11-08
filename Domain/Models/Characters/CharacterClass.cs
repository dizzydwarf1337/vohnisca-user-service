namespace Domain.Models.Characters;

public class CharacterClass
{
    public Guid Id { get; set; }
    public Guid CharacterId { get; set; }
    public Guid ClassId { get; set; }
    public Class Class { get; set; }
    
    public int Level { get; set; }
    
    public Guid? SubclassId { get; set; }
    public Subclass? Subclass { get; set; }
    
    public Dictionary<int, LevelChoices> ChoicesByLevel { get; set; } = new();
}