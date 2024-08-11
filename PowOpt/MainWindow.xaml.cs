using System.Windows;

namespace PowOpt
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // DataContext = устанавливается через DI, здесь это не нужно
        }
    }
}
