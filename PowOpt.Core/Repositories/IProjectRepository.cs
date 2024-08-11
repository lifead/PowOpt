using PowOpt.Core.Models;

namespace PowOpt.Core.Repositories
{
    public interface IProjectRepository
    {
        ProjectDataDbo LoadProject(string filePath);
    }
}