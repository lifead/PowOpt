using PowOpt.Core.Models;

namespace PowOpt.Core.Repositories
{
    public interface IProjectRepository
    {
        ProjectData LoadProject(string filePath);
    }
}