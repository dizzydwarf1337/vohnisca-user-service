using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class ResourcePool
{
    public ResourceType Type { get; set; }
    public int MaxAmount { get; set; }
    public int CurrentAmount { get; set; }
    public UsageType RechargeType { get; set; }
    public string DisplayName { get; set; }
    public string Description { get; set; }
    
    public DiceType? DiceType { get; set; }
    
    public string? Formula { get; set; }
    
    
    public bool Use(int amount = 1)
    {
        if (CurrentAmount < amount) return false;
        CurrentAmount -= amount;
        return true;
    }
    
    public void Restore(int amount)
    {
        CurrentAmount = Math.Min(CurrentAmount + amount, MaxAmount);
    }
    
    public void FullRestore()
    {
        CurrentAmount = MaxAmount;
    }
    
    public bool IsAvailable(int amount = 1)
    {
        return CurrentAmount >= amount;
    }
}