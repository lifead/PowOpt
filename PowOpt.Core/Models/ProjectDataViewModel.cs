using ReactiveUI;

namespace PowOpt.Core.Models
{
    public class ParameterViewModel : ReactiveObject
    {
        public int Id { get; set; }
        public string ParameterName { get; set; }
        public int GroupId { get; set; }  // Ссылка на идентификатор группы
    }

    public class GroupViewModel : ReactiveObject
    {
        public int Id { get; set; }  // Добавляем Id группы
        public string GroupName { get; set; }
        public List<ParameterViewModel> Items { get; set; }
    }

    public class ProjectDataViewModel
    {
        public List<GroupViewModel> Groups { get; set; }
    }
}