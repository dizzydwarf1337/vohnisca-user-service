
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Characters;

public class CharacterClass
{
    public Guid ClassId { get; set; }
    [ForeignKey(nameof(ClassId))]
    public virtual Class Class { get; set; }
    
    public Guid? SubClassId { get; set; }
    public virtual Subclass? Subclass{ get; set; }
    
    public int Level { get; set; }
    public Dictionary<int, LevelChoices> ChoicesByLevel { get; set; } = new();
    public Dictionary<int, int> HitPointsByLevel { get; set; } = new();
    

}