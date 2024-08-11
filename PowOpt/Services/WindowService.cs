using PowOpt.Core.Services;
using PowOpt.Core.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using PowOpt.Core.Models;
using PowOpt.Core.Repositories;
using System.Collections.ObjectModel;

namespace PowOpt.Services
{
    public class WindowService : IWindowService
    {
        private readonly IServiceProvider _serviceProvider;

        public WindowService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ShowEditMatrixWindow(string filePath)
        {
            // Получаем экземпляр EditMatrixViewModel через DI
            var editMatrixViewModel = _serviceProvider.GetRequiredService<EditMatrixViewModel>();

            // Устанавливаем параметры ViewModel (например, путь к файлу)
            editMatrixViewModel.FilePath = filePath;

            // Создаем и показываем окно
            var editMatrixWindow = new EditMatrixWindow(editMatrixViewModel);
            editMatrixWindow.ShowDialog();
        }

        public void ShowEditParameterWindow(ParameterViewModel parameter,
                                            ObservableCollection<GroupViewModel> availableGroups,
                                            ObservableCollection<ParameterTypeDbo> availableTypes,
                                            IProjectRepository projectRepository,
                                            string filePath,
                                            ProjectDataDbo projectData)
        {
            var editParameterWindow = new EditParameterWindow
            {
                DataContext = new EditParameterViewModel(parameter, availableGroups, availableTypes, projectRepository, filePath, projectData)
            };

            editParameterWindow.ShowDialog();
        }
    }
}
