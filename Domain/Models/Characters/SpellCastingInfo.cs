using Domain.Models.Characters.Enums;

namespace Domain.Models.Characters;

public class SpellcastingInfo
{
    public Guid ClassDefinitionId { get; set; } // От какого класса
    public SpellcastingType Type { get; set; } // Full/Half/Third/Pact
    public AbilityScore SpellcastingAbility { get; set; }
    
    // Вычисляемые значения
    public int SpellSaveDC { get; set; } // 8 + ProficiencyBonus + AbilityModifier
    public int SpellAttackBonus { get; set; } // ProficiencyBonus + AbilityModifier
    
    // Слоты заклинаний (уровень слота -> количество)
    public Dictionary<int, int> TotalSlots { get; set; } = new(); // Всего слотов
    public Dictionary<int, int> UsedSlots { get; set; } = new(); // Использованных
    
    // Известные заклинания (для классов типа Sorcerer, Bard)
    public List<Guid> KnownSpellIds { get; set; } = new();
    
    // Подготовленные заклинания (для классов типа Wizard, Cleric)
    public List<Guid> PreparedSpellIds { get; set; } = new();
    
    // Заклинания, которые всегда подготовлены (от подкласса/расы)
    public List<Guid> AlwaysPreparedSpellIds { get; set; } = new();
    
    // Количество известных кантрипов
    public int KnownCantrips { get; set; }
}