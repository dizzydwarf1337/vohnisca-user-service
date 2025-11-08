using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters.Items;

public class Weapon : Item
{
    public string DamageDice { get; set; }
    public DamageType DamageType { get; set; }
    public List<WeaponProperty> Properties { get; set; }
    public int RangeNormal { get; set; }
    public int RangeLong { get; set; }
    public string VersatileDamage { get; set; }
    public bool IsMagical { get; set; }
    public int MagicBonus { get; set; }
}