using PowOpt.Core.Models;
using PowOpt.Core.Services;
using PowOpt.Core.ViewModels;

namespace PowOpt.Services
{
    public class WindowService : IWindowService
    {
        public void ShowEditParameterWindow(ParameterViewModel parameter)
        {
            var editParameterWindow = new EditParameterWindow
            {
                DataContext = new EditParameterViewModel(parameter)
            };

            editParameterWindow.ShowDialog();
        }
    }
}
