using System.Collections.ObjectModel;

namespace PowOpt.Core.Models
{
    public class ParameterViewData : BindableBase
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int TypeId { get; set; }
        public decimal CalculatedValue { get; set; }

        private string _parameterName;
        public string ParameterName
        {
            get => _parameterName;
            set => SetProperty(ref _parameterName, value); // уведомление об изменении
        }

        private string _value;
        public string Value
        {
            get => _value;
            set => SetProperty(ref _value, value); // уведомление об изменении
        }
    }

    public class GroupViewData : BindableBase
    {
        public int Id { get; set; }  // Идентификатор группы
        public string GroupName { get; set; }

        private ObservableCollection<ParameterViewData> _items;
        public ObservableCollection<ParameterViewData> Items
        {
            get => _items;
            set => SetProperty(ref _items, value); // уведомление об изменении
        }

        public GroupViewData()
        {
            Items = new ObservableCollection<ParameterViewData>();
        }
    }

    public class ProjectDataViewData
    {
        public List<GroupViewData> Groups { get; set; }
    }
}