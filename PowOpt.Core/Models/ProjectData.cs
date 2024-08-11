namespace PowOpt.Core.Models
{
    public class Parameter
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Group
    {
        public string GroupName { get; set; }
        public List<Parameter> Items { get; set; }
    }

    public class ProjectData
    {
        public List<Group> Groups { get; set; }
    }
}
