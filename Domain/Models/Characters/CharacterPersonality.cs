namespace Domain.Models.Characters;

public class CharacterPersonality
{
    public string PersonalityTraits { get; set; } = string.Empty;
    public string Ideals { get; set; } = string.Empty;
    public string Bonds { get; set; } = string.Empty;
    public string Flaws { get; set; } = string.Empty;
    public string Backstory { get; set; } = string.Empty;

    public List<string> AlliesAndOrganizations { get; set; } = new();
    public List<string> Enemies { get; set; } = new();
}