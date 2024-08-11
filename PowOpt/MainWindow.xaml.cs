using PowOpt.Core.Repositories;
using PowOpt.Core.ViewModels;
using System.Windows;

namespace PowOpt
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Создание репозитория вручную
            var projectRepository = new JsonProjectRepository();

            // Создание ViewModel с передачей репозитория
            var mainViewModel = new MainViewModel(projectRepository);

            // Установка DataContext
            DataContext = mainViewModel;
        }
    }
}
