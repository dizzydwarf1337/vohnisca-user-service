namespace Domain.Models.Characters.Leveling;

public class FeatureChoiceProgression
{
    public string ChoiceName { get; set; }
    public int NumberOfChoices { get; set; }
    public bool CanReplaceOnLevelUp { get; set; }
    public virtual ICollection<Feature> AvailableFeatures { get; set; }
}