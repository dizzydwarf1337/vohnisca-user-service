namespace Domain.Models.Characters.Enums;

public enum EffectType
{
    Bonus,              // Бонус к чему-то (+2 AC, +1d6 damage)
    SetValue,           // Установить значение (AC = 13 + Dex для Monk)
    Resistance,         // Сопротивление урону
    Immunity,           // Иммунитет
    Vulnerability,      // Уязвимость
    Advantage,          // Преимущество на броски
    Disadvantage,       // Помеха
    ExtraAction,        // Дополнительное действие (Extra Attack)
    Proficiency,        // Профа в чем-то
    GrantSpell,         // Дает заклинание
    ModifyResource,     // Изменяет ресурс
    Custom              // Кастомный эффект (текстовое описание)
}