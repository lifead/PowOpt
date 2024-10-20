using MathNet.Symbolics;
using Expr = MathNet.Symbolics.SymbolicExpression;

namespace PowOpt.Core.Services
{
    public class FormulaCalculationService : IFormulaCalculationService
    {
        public decimal Calculate(string formula, Dictionary<string, decimal> variables)
        {
            try
            {
                var expression = Expr.Parse(formula);

                // Используем FloatingPoint для преобразования значений и строки для имен переменных
                var symbolValues = variables.ToDictionary(
                    kv => kv.Key,  // Используем строковые имена переменных
                    kv => FloatingPoint.NewReal((double)kv.Value) // Преобразуем значения в FloatingPoint
                );

                var result = expression.Evaluate(symbolValues); // Ожидает словарь с ключами типа string и значениями FloatingPoint

                // Преобразуем результат в decimal
                return (decimal)result.RealValue;
            }
            catch (Exception ex)
            {
                // Логируем или обрабатываем ошибки
                throw new InvalidOperationException($"Ошибка при вычислении формулы: {formula}", ex);
            }
        }
    }
}
