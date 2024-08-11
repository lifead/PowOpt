namespace PowOpt.Core.Models
{
    public class ProjectData
    {
        public List<string> Parameters { get; set; } = new List<string>();
        public List<string> Formulas { get; set; } = new List<string>();
        public List<string> Charts { get; set; } = new List<string>();
    }
}