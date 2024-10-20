using PowOpt.Core.Models;
using System.Text.Json;

namespace PowOpt.Core.Repositories;

public class JsonProjectRepository : IProjectRepository
{
    public ProjectDataDbo LoadProject(string filePath)
    {
        if (!File.Exists(filePath)) return null;

        var json = File.ReadAllText(filePath);
        var projectData = JsonSerializer.Deserialize<ProjectDataDbo>(json);

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
