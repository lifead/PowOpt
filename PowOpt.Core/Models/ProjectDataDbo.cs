namespace PowOpt.Core.Models
{
    public class GroupDbo
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
    }

    public class ParameterDbo
    {
        public int Id { get; set; }
        public string ParameterName { get; set; }
        public int GroupId { get; set; }
    }

    public class ProjectDataDbo
    {
        public List<GroupDbo> Groups { get; set; }
        public List<ParameterDbo> Parameters { get; set; }
    }
}
