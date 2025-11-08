using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters.Items;

public class Weapon : Item
{
    public DiceType DamageDice { get; set; }
    public int DamageDiceCount { get; set; } = 1;
    public DamageType DamageType { get; set; }
    public List<WeaponProperty> Properties { get; set; }
    public int RangeNormal { get; set; }
    public int RangeLong { get; set; }
    public DiceType? VersatileDiceType { get; set; }
    public int? VersatileDiceCount { get; set; }
    public bool IsMagical { get; set; }
    public int MagicBonus { get; set; }
}