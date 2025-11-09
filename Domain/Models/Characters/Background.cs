using Domain.Models.Characters.Enums;
using Domain.Models.Characters.Items;

namespace Domain.Models.Characters;

public class Background
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Source { get; set; }
    
    public List<Skill> SkillProficiencies { get; set; } = new();
    
    public List<ToolProficiency> ToolProficiencies { get; set; } = new();
    
    public int AvailableLanguages { get; set; }
    public List<Language> GrantedLanguages { get; set; } = new(); 
    
    public List<Item> StartingEquipment { get; set; } = new();
    public Currency StartingMoney { get; set; } = new();
    
    public Guid? BackgroundFeatureId { get; set; } 
    
    public virtual ICollection<Character> Characters { get; set; }
}