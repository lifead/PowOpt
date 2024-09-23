using PowOpt.Core.Models;
using PowOpt.Core.Repositories;
using PowOpt.Core.Services;
using PowOpt.Core.ViewModels;
using System.Collections.ObjectModel;

namespace PowOpt.Services
{
    public class WindowService : IWindowService
    {
        private readonly IContainerProvider _containerProvider;

        // Заменяем IServiceProvider на IContainerProvider (Prism)
        public WindowService(IContainerProvider containerProvider)
        {
            _containerProvider = containerProvider;
        }

        public void ShowEditMatrixWindow(string filePath)
        {
            // Получаем экземпляр EditMatrixViewModel через контейнер Prism
            var editMatrixViewModel = _containerProvider.Resolve<EditMatrixViewModel>();

            // Устанавливаем параметры ViewModel (например, путь к файлу)
            editMatrixViewModel.FilePath = filePath;

            // Создаем и показываем окно, передавая ViewModel через конструктор
            var editMatrixWindow = new EditMatrixWindow(editMatrixViewModel);
            editMatrixWindow.ShowDialog();
        }

        public void ShowEditParameterWindow(ParameterViewData parameter,
                                            ObservableCollection<GroupViewData> availableGroups,
                                            ObservableCollection<ParameterTypeDbo> availableTypes,
                                            IProjectRepository projectRepository,
                                            string filePath,
                                            ProjectDataDbo projectData)
        {
            // Вручную создаем экземпляр EditParameterViewModel, так как у нас есть параметры
            var editParameterViewModel = new EditParameterViewModel(parameter, availableGroups, availableTypes, projectRepository, filePath, projectData);

            var editParameterWindow = new EditParameterWindow
            {
                DataContext = editParameterViewModel
            };

            editParameterWindow.ShowDialog();
        }
    }
}
