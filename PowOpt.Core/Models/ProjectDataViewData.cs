using ReactiveUI;

namespace PowOpt.Core.Models
{
    public class ParameterViewData : ReactiveObject
    {
        public int Id { get; set; }
        public string ParameterName { get; set; }
        public int GroupId { get; set; }
        public int TypeId { get; set; }
        public string Value { get; set; }  // Значение параметра
        public decimal CalculatedValue { get; set; }  // Значение параметра
    }

    public class GroupViewData : ReactiveObject
    {
        public int Id { get; set; }  // Идентификатор группы
        public string GroupName { get; set; }
        public List<ParameterViewData> Items { get; set; }
    }

    public class ProjectDataViewData
    {
        public List<GroupViewData> Groups { get; set; }
    }
}