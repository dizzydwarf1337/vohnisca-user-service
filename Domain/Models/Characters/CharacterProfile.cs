namespace Domain.Models.Characters;

public class CharacterProfile
{
    public int Height { get; set; }
    public int Weight { get; set; }
    public string Eyes { get; set; } = string.Empty;
    public string Skin { get; set; } = string.Empty;
    public string Hair { get; set; } = string.Empty;
    public string AvatarUrl { get; set; } = string.Empty;
}
