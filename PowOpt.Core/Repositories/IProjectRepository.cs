using PowOpt.Core.Models;

namespace PowOpt.Core.Repositories
{
    public interface IProjectRepository
    {
        ProjectDataDbo LoadProject(string filePath);
        void SaveProject(string filePath, ProjectDataDbo projectData);

        // Методы для работы с данными матрицы
        MatrixDataDbo LoadMatrixData(string filePath);
        void SaveMatrixData(string filePath, MatrixDataDbo matrixData);
    }
}
