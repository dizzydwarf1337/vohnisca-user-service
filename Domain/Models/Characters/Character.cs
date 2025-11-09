using Domain.Models.Characters.Enums;
using Domain.Models.Users;

namespace Domain.Models.Characters;

public class Character
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
        
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public string Name { get; set; } = string.Empty;
    
    public Guid RaceId { get; set; }
    public virtual Race  Race { get; set; }
    public SubRace? SubRace { get; set; }
    public Guid BackgroundId { get; set; }
    public virtual Background Background { get; set; }
    
    public Alignment Alignment { get; set; }
    
    public Size Size { get; set; }
    public int Age { get; set; }
    
    public CharacterProfile Profile { get; set; }
    
    public List<CharacterClass> Classes { get; set; } = new();
    
    public CharacterStats Stats { get; set; } = new();
    
    public CharacterCombat Combat { get; set; } = new();
    
    public CharacterMagic Magic { get; set; } = new();
    
    public CharacterInventory Inventory { get; set; } = new();
    
    public CharacterPersonality Personality { get; set; } = new();
    
    public List<HitDiceEntry> HitDice { get; set; } = new();
    
    public List<CharacterAction> AvailableActions { get; set; } = new();

    public ICollection<Feature> SelectedFeatures { get; set; } = new List<Feature>();
    public virtual User User { get; set; }
    
    public int GetTotalLevel()
    {
        return Classes.Sum(c => c.Level);
    }

    public int GetCarryingCapacity()
    {
        return Stats.GetCarryingCapacity();
    }
    
    public int GetInitiative()
    {
        return Stats.GetInitiative();
    }

    public int GetPassivePerception()
    {
        return Stats.GetPassivePerception();
    }
    
    public double GetCurrentWeight()
    {
        return Inventory.TotalWeight;
    }
    
    public bool IsEncumbered()
    {
        return GetCurrentWeight() > GetCarryingCapacity();
    }
}