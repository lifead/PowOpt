using PowOpt.Core.Models;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;

namespace PowOpt.Core.ViewModels
{
    public class EditParameterViewModel : ReactiveObject
    {
        private string _parameterName;
        public string ParameterName
        {
            get => _parameterName;
            set => this.RaiseAndSetIfChanged(ref _parameterName, value);
        }

        public int ParameterId { get; }  // Добавляем свойство для отображения идентификатора

        private GroupViewModel _selectedGroup;
        public GroupViewModel SelectedGroup
        {
            get => _selectedGroup;
            set => this.RaiseAndSetIfChanged(ref _selectedGroup, value);
        }

        public ObservableCollection<GroupViewModel> AvailableGroups { get; }

        public ReactiveCommand<Unit, Unit> SaveCommand { get; }

        public EditParameterViewModel(ParameterViewModel parameter, ObservableCollection<GroupViewModel> availableGroups)
        {
            ParameterId = parameter.Id;  // Устанавливаем значение идентификатора
            ParameterName = parameter.ParameterName;
            AvailableGroups = availableGroups;

            // Найти и установить выбранную группу по GroupId
            SelectedGroup = AvailableGroups.FirstOrDefault(g => g.Id == parameter.GroupId);

            SaveCommand = ReactiveCommand.Create(Save);
        }

        private void Save()
        {
            // Логика сохранения параметра, например, обновление данных в базе или файле.
            // Здесь нужно будет обновить группу и имя параметра в зависимости от измененных данных.
        }
    }
}
