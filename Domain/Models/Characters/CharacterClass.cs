using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Characters;

public class CharacterClass
{
    public Guid ClassId { get; set; }
    public Guid? SubclassId { get; set; }
    
    public int Level { get; set; }
    public Dictionary<int, LevelChoices> ChoicesByLevel { get; set; } = new();
    public Dictionary<int, int> HitPointsByLevel { get; set; } = new();
    
    [ForeignKey(nameof(ClassId))]
    public virtual Class Class { get; set; }
    [ForeignKey(nameof(SubclassId))]
    public virtual Subclass? Subclass { get; set; }
}