using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters.Items;

public class Armor : Item
{
    public ArmorType ArmorType { get; set; }
    public int BaseArmorClass { get; set; }
    public bool AddDexModifier { get; set; }
    public int MaxDexModifier { get; set; }
    public int StrengthRequirement { get; set; }
    public bool HasStealthDisadvantage { get; set; }
    public bool IsMagical { get; set; }
    public int MagicBonus { get; set; }
}