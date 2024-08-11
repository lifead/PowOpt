using PowOpt.Core.Repositories;
using PowOpt.Core.ViewModels;
using System.Windows;

namespace PowOpt;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        // Создаем репозиторий
        var projectRepository = new JsonProjectRepository();

        // Передаем репозиторий в ViewModel
        DataContext = new MainViewModel(projectRepository);
    }
}