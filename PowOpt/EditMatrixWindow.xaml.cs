using System.Windows;
using PowOpt.Core.ViewModels;

namespace PowOpt
{
    public partial class EditMatrixWindow : Window
    {
        public EditMatrixWindow(EditMatrixViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}