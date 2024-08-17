using PowOpt.Core.Models;
using System.Text.Json;
using NCalc;

namespace PowOpt.Core.Repositories
{
    public class JsonProjectRepository : IProjectRepository
    {
        public ProjectDataDbo LoadProject(string filePath)
        {
            if (!File.Exists(filePath)) return null;

            var json = File.ReadAllText(filePath);
            var projectData = JsonSerializer.Deserialize<ProjectDataDbo>(json);

            // Проходим по всем параметрам и вычисляем значения для формул
            foreach (var parameter in projectData.Parameters)
            {
                if (parameter.TypeId == 2) // Предположим, что TypeId для формул равен 2
                {
                    parameter.CalculatedValue = CalculateFormula(parameter.Value);
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

        private decimal CalculateFormula(string formula)
        {
            try
            {
                var expression = new Expression(formula);
                var result = expression.Evaluate();
                return Convert.ToDecimal(result);
            }
            catch (Exception ex)
            {
                // Обработка ошибок, например, логирование
                Console.WriteLine($"Ошибка при вычислении формулы: {ex.Message}");
                return 0; // Можно вернуть значение по умолчанию или обработать ошибку иначе
            }
        }
    }
}
