using MathNet.Symbolics;
using PowOpt.Core.Models;
using System.Globalization;
using Expr = MathNet.Symbolics.SymbolicExpression;

namespace PowOpt.Core.Services;

public class FormulaCalculationService : IFormulaCalculationService
{
    public ProjectDataDbo Calculate(ProjectDataDbo projectData)
    {
        // Подготовка словаря с переменными для вычисления (без формул)
        var variableDictionary = projectData.Parameters
            .Where(p => p.TypeId != 3)
            .ToDictionary(
                p => p.ParameterName,
                p => decimal.Parse(p.Value, CultureInfo.InvariantCulture) // Используем инвариантную культуру
            );

        // Вычисляем значения для формул и числовых параметров
        foreach (var parameter in projectData.Parameters)
        {
            if (parameter.TypeId == 3) // Если это формула
            {
                parameter.CalculatedValue = CalculateFormula(parameter.Value, variableDictionary);
            }
            else if (parameter.TypeId == 1 || parameter.TypeId == 2) // Для числовых значений
            {
                parameter.CalculatedValue = decimal.Parse(parameter.Value, CultureInfo.InvariantCulture);
            }

            // Обновляем словарь для использования в дальнейших формулах
            variableDictionary[parameter.ParameterName] = parameter.CalculatedValue;
        }

        return projectData;
    }

    private decimal CalculateFormula(string formula, Dictionary<string, decimal> variables)
    {
        try
        {
            var expression = Expr.Parse(formula);

            // Используем FloatingPoint для преобразования значений и строки для имен переменных
            var symbolValues = variables.ToDictionary(
                kv => kv.Key,
                kv => FloatingPoint.NewReal((double)kv.Value)
            );

            var result = expression.Evaluate(symbolValues);

            // Преобразуем результат в decimal
            return (decimal)result.RealValue;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Ошибка при вычислении формулы: {formula}", ex);
        }
    }
}
