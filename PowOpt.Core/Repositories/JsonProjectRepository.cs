using PowOpt.Core.Models;
using PowOpt.Core.Services;
using System.Globalization;
using System.Text.Json;

namespace PowOpt.Core.Repositories;

public class JsonProjectRepository : IProjectRepository
{
    private readonly IFormulaCalculationService _formulaCalculationService;

    public JsonProjectRepository(IFormulaCalculationService formulaCalculationService)
    {
        _formulaCalculationService = formulaCalculationService;
    }

    public ProjectDataDbo LoadProject(string filePath)
    {
        if (!File.Exists(filePath)) return null;

        var json = File.ReadAllText(filePath);
        var projectData = JsonSerializer.Deserialize<ProjectDataDbo>(json);

        // Подготовка словаря с переменными для вычисления
        var variableDictionary = projectData.Parameters
            .Where(p => p.TypeId != 3)
            .ToDictionary(
                p => p.ParameterName,
                p => decimal.Parse(p.Value, CultureInfo.InvariantCulture) // Указываем инвариантную культуру
            );

        // Вычисляем значения для формул
        foreach (var parameter in projectData.Parameters)
        {
            if (parameter.TypeId == 3) // Формула
            {
                parameter.CalculatedValue = _formulaCalculationService.Calculate(parameter.Value, variableDictionary);
            }
        }

        return projectData;
    }

    public void SaveProject(string filePath, ProjectDataDbo projectData)
    {
        var json = JsonSerializer.Serialize(projectData, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }

    public MatrixDataDbo LoadMatrixData(string filePath)
    {
        if (!File.Exists(filePath)) return null;

        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<MatrixDataDbo>(json);
    }

    public void SaveMatrixData(string filePath, MatrixDataDbo matrixData)
    {
        var json = JsonSerializer.Serialize(matrixData, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }
}
