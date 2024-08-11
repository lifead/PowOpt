using PowOpt.Core.ViewModels;
using System.Collections.ObjectModel;
using PowOpt.Core.Models;
using PowOpt.Core.Repositories;

namespace PowOpt.Core.Services
{
    public interface IWindowService
    {
        void ShowEditParameterWindow(ParameterViewModel parameter,
                                     ObservableCollection<GroupViewModel> availableGroups,
                                     ObservableCollection<ParameterTypeDbo> availableTypes,
                                     IProjectRepository projectRepository,
                                     string filePath,
                                     ProjectDataDbo projectData);
    }
}
