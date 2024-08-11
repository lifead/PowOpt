using PowOpt.Core.Models;
using PowOpt.Core.Services;
using PowOpt.Core.ViewModels;
using System.Collections.ObjectModel;

namespace PowOpt.Services
{
    public class WindowService : IWindowService
    {
        public void ShowEditParameterWindow(ParameterViewModel parameter, ObservableCollection<GroupViewModel> availableGroups)
        {
            var editParameterWindow = new EditParameterWindow
            {
                DataContext = new EditParameterViewModel(parameter, availableGroups)
            };

            editParameterWindow.ShowDialog();
        }
    }
}
