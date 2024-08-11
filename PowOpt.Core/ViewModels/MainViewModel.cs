using PowOpt.Core.Models;
using PowOpt.Core.Repositories;
using PowOpt.Core.Services;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;

namespace PowOpt.Core.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        private readonly IProjectRepository _projectRepository;
        private readonly DataTransformationService _dataTransformationService;
        private readonly IWindowService _windowService;

        private ObservableCollection<GroupViewModel> _displayGroups;
        public ObservableCollection<GroupViewModel> DisplayGroups
        {
            get => _displayGroups;
            set => this.RaiseAndSetIfChanged(ref _displayGroups, value);
        }

        public ObservableCollection<ParameterTypeDbo> ParameterTypes { get; private set; }

        public ParameterViewModel SelectedParameter { get; set; }

        public ReactiveCommand<Unit, Unit> OpenProjectCommand { get; }
        public ReactiveCommand<Unit, Unit> EditParameterCommand { get; }
        public ReactiveCommand<Unit, Unit> EditMatrixCommand { get; }

        public MainViewModel(IProjectRepository projectRepository,
                             DataTransformationService dataTransformationService,
                             IWindowService windowService)
        {
            _projectRepository = projectRepository;
            _dataTransformationService = dataTransformationService;
            _windowService = windowService;

            DisplayGroups = new ObservableCollection<GroupViewModel>();
            ParameterTypes = new ObservableCollection<ParameterTypeDbo>();

            OpenProjectCommand = ReactiveCommand.Create(OpenProject);
            EditParameterCommand = ReactiveCommand.Create(EditParameter);
            EditMatrixCommand = ReactiveCommand.Create(OpenEditMatrixWindow);
        }

        private void OpenProject()
        {
            string filePath = "projectData.json";
            var data = _projectRepository.LoadProject(filePath);

            if (data != null)
            {
                var displayData = _dataTransformationService.TransformToViewModel(data);

                DisplayGroups.Clear();
                foreach (var group in displayData.Groups)
                {
                    DisplayGroups.Add(group);
                }

                ParameterTypes.Clear();
                foreach (var type in data.ParameterTypes)
                {
                    ParameterTypes.Add(type);
                }
            }
        }

        private void EditParameter()
        {
            if (SelectedParameter == null) return;

            // Загружаем текущие данные проекта для редактирования
            var projectData = _projectRepository.LoadProject("projectData.json");

            // Открываем окно редактирования параметра
            _windowService.ShowEditParameterWindow(
                SelectedParameter,
                DisplayGroups,
                ParameterTypes,
                _projectRepository,
                "projectData.json",
                projectData
            );

            // Перезагружаем данные после редактирования
            OpenProject();
        }

        private void OpenEditMatrixWindow()
        {
            _windowService.ShowEditMatrixWindow();
        }
    }
}
