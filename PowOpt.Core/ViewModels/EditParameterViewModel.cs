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
        private readonly ParameterViewModel _parameter;
        private readonly ProjectDataDbo _projectData;

        private string _parameterName;
        public string ParameterName
        {
            get => _parameterName;
            set => this.RaiseAndSetIfChanged(ref _parameterName, value);
        }

        public int ParameterId { get; }

        private GroupViewModel _selectedGroup;
        public GroupViewModel SelectedGroup
        {
            get => _selectedGroup;
            set => this.RaiseAndSetIfChanged(ref _selectedGroup, value);
        }

        public ObservableCollection<GroupViewModel> AvailableGroups { get; }

        public ReactiveCommand<Unit, Unit> SaveCommand { get; }

        public EditParameterViewModel(ParameterViewModel parameter, ObservableCollection<GroupViewModel> availableGroups, IProjectRepository projectRepository, string filePath, ProjectDataDbo projectData)
        {
            _parameter = parameter;
            _projectRepository = projectRepository;
            _filePath = filePath;
            _projectData = projectData;

            ParameterId = parameter.Id;
            ParameterName = parameter.ParameterName;
            AvailableGroups = availableGroups;

            // Найти и установить выбранную группу по GroupId
            SelectedGroup = AvailableGroups.FirstOrDefault(g => g.Id == parameter.GroupId);

            SaveCommand = ReactiveCommand.Create(Save);
        }

        private void Save()
        {
            // Обновляем данные параметра
            var parameterDbo = _projectData.Parameters.FirstOrDefault(p => p.Id == ParameterId);
            if (parameterDbo != null)
            {
                parameterDbo.ParameterName = ParameterName;
                parameterDbo.GroupId = SelectedGroup?.Id ?? parameterDbo.GroupId;
            }

            // Сохраняем изменения в файл
            _projectRepository.SaveProject(_filePath, _projectData);
        }
    }
}
