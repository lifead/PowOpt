using PowOpt.Core.Models;
using PowOpt.Core.ViewModels;
using System.Collections.ObjectModel;

namespace PowOpt.Core.Services
{
    public interface IWindowService
    {
        void ShowEditParameterWindow(ParameterViewModel parameter, ObservableCollection<GroupViewModel> availableGroups);
    }
}