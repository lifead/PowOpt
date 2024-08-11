using PowOpt.Core.Services;
using PowOpt.Core.ViewModels;
using System.Collections.ObjectModel;
using PowOpt.Core.Models;
using PowOpt.Core.Repositories;

namespace PowOpt.Services
{
    public class WindowService : IWindowService
    {
        public void ShowEditMatrixWindow()
        {
            var editMatrixWindow = new EditMatrixWindow();
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
