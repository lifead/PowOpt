using PowOpt.Core.Models;
using PowOpt.Core.Repositories;
using PowOpt.Core.ViewModels;
using System.Collections.ObjectModel;

namespace PowOpt.Core.Services
{
    public interface IWindowService
    {
        void ShowEditMatrixWindow(string filePath); // Новый метод
        void ShowEditParameterWindow(ParameterViewModel parameter,
                                     ObservableCollection<GroupViewModel> availableGroups,
                                     ObservableCollection<ParameterTypeDbo> availableTypes,
                                     IProjectRepository projectRepository,
                                     string filePath,
                                     ProjectDataDbo projectData);
    }
}
