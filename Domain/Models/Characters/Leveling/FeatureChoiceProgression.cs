namespace Domain.Models.Characters.Leveling;

public class FeatureChoiceProgression
{
    public string ChoiceName { get; set; }
    public int NumberOfChoices { get; set; }
    public List<Guid> AvailableFeatureIds { get; set; }
    public bool CanReplaceOnLevelUp { get; set; }
}