using PowOpt.Core.Models;
using PowOpt.Core.Repositories;
using PowOpt.Core.Services;
using System.Collections.ObjectModel;

namespace PowOpt.Core.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly DataTransformationService _dataTransformationService;
        private readonly IWindowService _windowService;

        private ObservableCollection<GroupViewData> _displayGroups;
        public ObservableCollection<GroupViewData> DisplayGroups
        {
            get => _displayGroups;
            set => SetProperty(ref _displayGroups, value); // Prism's SetProperty для уведомления об изменениях
        }

        public ObservableCollection<ParameterTypeDbo> ParameterTypes { get; private set; }

        private ParameterViewData _selectedParameter;
        public ParameterViewData SelectedParameter
        {
            get => _selectedParameter;
            set => SetProperty(ref _selectedParameter, value);
        }

        // Замена ReactiveCommand на DelegateCommand
        public DelegateCommand OpenProjectCommand { get; }
        public DelegateCommand EditParameterCommand { get; }
        public DelegateCommand EditMatrixCommand { get; }

        public MainViewModel(IProjectRepository projectRepository,
                             DataTransformationService dataTransformationService,
                             IWindowService windowService)
        {
            _projectRepository = projectRepository;
            _dataTransformationService = dataTransformationService;
            _windowService = windowService;

            DisplayGroups = new ObservableCollection<GroupViewData>();
            ParameterTypes = new ObservableCollection<ParameterTypeDbo>();

            // Инициализация команд DelegateCommand
            OpenProjectCommand = new DelegateCommand(OpenProject);
            EditParameterCommand = new DelegateCommand(EditParameter, CanEditParameter)
                                    .ObservesProperty(() => SelectedParameter); // Команда зависит от SelectedParameter
            EditMatrixCommand = new DelegateCommand(OpenEditMatrixWindow);
        }

        // Метод для загрузки проекта
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

        // Проверка, можно ли редактировать параметр
        private bool CanEditParameter()
        {
            return SelectedParameter != null;
        }

        // Метод для редактирования параметра
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

        // Метод для открытия окна редактирования матрицы
        private void OpenEditMatrixWindow()
        {
            string matrixFilePath = "matrixData.json"; // Укажите путь к файлу данных матрицы
            _windowService.ShowEditMatrixWindow(matrixFilePath);
        }
    }
}
