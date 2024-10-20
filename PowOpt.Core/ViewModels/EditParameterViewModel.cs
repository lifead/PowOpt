using PowOpt.Core.Models;
using PowOpt.Core.Repositories;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;

namespace PowOpt.Core.ViewModels
{
    public class EditParameterViewModel : ReactiveObject
    {
        private readonly IProjectRepository _projectRepository;
        private readonly string _filePath;
        private readonly ParameterViewData _parameter;
        private readonly ProjectDataDbo _projectData;

        private string _parameterName;
        public string ParameterName
        {
            get => _parameterName;
            set => this.RaiseAndSetIfChanged(ref _parameterName, value);
        }

        public int ParameterId { get; }

        private GroupViewData _selectedGroup;
        public GroupViewData SelectedGroup
        {
            get => _selectedGroup;
            set => this.RaiseAndSetIfChanged(ref _selectedGroup, value);
        }

        private string _value;
        public string Value
        {
            get => _value;
            set => this.RaiseAndSetIfChanged(ref _value, value);
        }

        private decimal _calculatedValue;
        public decimal CalculatedValue
        {
            get => _calculatedValue;
            set => this.RaiseAndSetIfChanged(ref _calculatedValue, value);
        }


        private int _selectedTypeId;
        public int SelectedTypeId
        {
            get => _selectedTypeId;
            set => this.RaiseAndSetIfChanged(ref _selectedTypeId, value);
        }

        public ObservableCollection<GroupViewData> AvailableGroups { get; }
        public ObservableCollection<ParameterTypeDbo> AvailableTypes { get; }

        public ReactiveCommand<Unit, Unit> SaveCommand { get; }

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
            CalculatedValue = parameter.CalculatedValue;
            AvailableGroups = availableGroups;
            AvailableTypes = availableTypes;

            SelectedGroup = AvailableGroups.FirstOrDefault(g => g.Id == parameter.GroupId);
            SelectedTypeId = parameter.TypeId;

            SaveCommand = ReactiveCommand.Create(Save);
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
