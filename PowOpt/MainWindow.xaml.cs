using System.Windows;
using System.Windows.Controls;
using PowOpt.Core.Models;
using PowOpt.Core.ViewModels;

namespace PowOpt
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NavigationTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var viewModel = DataContext as MainViewModel;
            if (viewModel != null)
            {
                viewModel.SelectedParameter = e.NewValue as ParameterViewModel;
            }
        }
    }
}
