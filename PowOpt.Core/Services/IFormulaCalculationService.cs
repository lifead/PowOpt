namespace PowOpt.Core.Services;

public interface IFormulaCalculationService
{
    decimal Calculate(string formula, Dictionary<string, decimal> variables);
}
