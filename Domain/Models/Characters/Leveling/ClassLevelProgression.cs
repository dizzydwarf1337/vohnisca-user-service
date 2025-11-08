namespace Domain.Models.Characters.Leveling;

public class ClassLevelProgression
{
    public int Level { get; set; }
    
    // Ресурсы, которые получает/обновляет персонаж
    public List<ResourceProgression> ResourceProgressions { get; set; }
    
    // Фичи, которые получает
    public List<Guid> FeatureIds { get; set; }
    
    // Выборы
    public List<FeatureChoiceProgression> FeatureChoices { get; set; }
    
    // ASI на этом уровне?
    public bool GrantsASI { get; set; }
}