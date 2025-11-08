namespace Domain.Models.Characters.Enums;

public enum ResourceProgressionAction
{
    Set,        // Установить значение (новый ресурс или перезапись)
    Increase,   // Увеличить существующий
    Decrease,   // Уменьшить существующий
    SetMax,     // Установить только максимум
    SetFormula  // Установить формулу для вычисления
}