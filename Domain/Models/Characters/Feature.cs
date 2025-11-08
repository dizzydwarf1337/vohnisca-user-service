using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class Feature
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public FeatureType Type { get; set; }
    
    // Требования (опционально)
    public FeatureRequirements? Requirements { get; set; }
    
    // Эффекты
    public List<FeatureEffect> Effects { get; set; }
    
    // Ограничения использования
    public FeatureUsage? Usage { get; set; }
}