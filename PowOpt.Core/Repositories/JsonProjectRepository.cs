using PowOpt.Core.Models;
using System.IO;
using System.Text.Json;

namespace PowOpt.Core.Repositories
{
    public class JsonProjectRepository : IProjectRepository
    {
        public ProjectDataDbo LoadProject(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<ProjectDataDbo>(json);
        }
    }
}
