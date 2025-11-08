using Domain.Models.Characters.Enums;
using Domain.Models.Characters.Items;

namespace Domain.Models.Characters;

public class Background
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Source { get; set; }
    public List<Skill> Skills { get; set; }
    public int AvailableLanguages { get; set; }
    public List<Item> Items { get; set; }
    public Currency Money { get; set; }
}