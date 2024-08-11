namespace PowOpt.Core.Models
{
    public class ParameterViewModel
    {
        public int Id { get; set; }
        public string ParameterName { get; set; }
    }

    public class GroupViewModel
    {
        public string GroupName { get; set; }
        public List<ParameterViewModel> Items { get; set; }
    }

    public class ProjectDataViewModel
    {
        public List<GroupViewModel> Groups { get; set; }
    }
}