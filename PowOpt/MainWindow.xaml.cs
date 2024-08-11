using PowOpt.Core.ViewModels;
using System.Windows;

namespace PowOpt
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = mainViewModel;
        }
    }
}
