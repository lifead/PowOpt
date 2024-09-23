using PowOpt.Core.Models;
using PowOpt.Core.Repositories;
using System.Collections.ObjectModel;

namespace PowOpt.Core.ViewModels
{
    public class EditParameterViewModel : BindableBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly string _filePath;
        private readonly ParameterViewData _parameter;
        private readonly ProjectDataDbo _projectData;

        private string _parameterName;
        public string ParameterName
        {
            get => _parameterName;
            set => SetProperty(ref _parameterName, value); // Prism's SetProperty для уведомления об изменениях
        }

        public int ParameterId { get; }

        private GroupViewData _selectedGroup;
        public GroupViewData SelectedGroup
        {
            get => _selectedGroup;
            set => SetProperty(ref _selectedGroup, value);
        }

        private string _value;
        public string Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        private int _selectedTypeId;
        public int SelectedTypeId
        {
            get => _selectedTypeId;
            set => SetProperty(ref _selectedTypeId, value);
        }

        public ObservableCollection<GroupViewData> AvailableGroups { get; }
        public ObservableCollection<ParameterTypeDbo> AvailableTypes { get; }

        public DelegateCommand SaveCommand { get; }

        public EditParameterViewModel(ParameterViewData parameter,
                                      ObservableCollection<GroupViewData> availableGroups,
                                      ObservableCollection<ParameterTypeDbo> availableTypes,
                                      IProjectRepository projectRepository,
                                      string filePath,
                                      ProjectDataDbo projectData)
        {
            _parameter = parameter;
            _projectRepository = projectRepository;
            _filePath = filePath;
            _projectData = projectData;

            ParameterId = parameter.Id;
            ParameterName = parameter.ParameterName;
            Value = parameter.Value;
            AvailableGroups = availableGroups;
            AvailableTypes = availableTypes;

            SelectedGroup = AvailableGroups.FirstOrDefault(g => g.Id == parameter.GroupId) ?? throw new NullReferenceException("SelectedGroup is null");
            SelectedTypeId = parameter.TypeId;

            // Инициализация команды Save
            SaveCommand = new DelegateCommand(Save);
        }

        private void Save()
        {
            // Обновляем данные параметра в исходной структуре данных (ProjectDataDbo)
            var parameterDbo = _projectData.Parameters.FirstOrDefault(p => p.Id == ParameterId);
            if (parameterDbo != null)
            {
                parameterDbo.ParameterName = ParameterName;
                parameterDbo.GroupId = SelectedGroup?.Id ?? parameterDbo.GroupId;
                parameterDbo.TypeId = SelectedTypeId;
                parameterDbo.Value = Value;
            }

            // Сохраняем изменения в файл
            _projectRepository.SaveProject(_filePath, _projectData);
        }
    }
}
