using PowOpt.Core.Models;
using System.Text.Json;

namespace PowOpt.Core.Repositories
{
    public class JsonProjectRepository : IProjectRepository
    {
        public ProjectData LoadProject(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<ProjectData>(json);
        }
    }
}
