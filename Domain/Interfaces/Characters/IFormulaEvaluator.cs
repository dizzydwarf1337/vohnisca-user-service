using Domain.Models.Characters;

namespace Domain.Interfaces.Characters;

public interface IFormulaEvaluator
{
    int Evaluate(string formula, Character character);
}