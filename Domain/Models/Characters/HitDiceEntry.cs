using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class HitDiceEntry
{
    public DiceType DiceType { get; set; }
    public int Total { get; set; }
    public int Used { get; set; }
}