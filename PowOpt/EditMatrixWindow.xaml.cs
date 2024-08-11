using PowOpt.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PowOpt
{
    /// <summary>
    /// Interaction logic for EditMatrixWindow.xaml
    /// </summary>
    public partial class EditMatrixWindow : Window
    {
        public EditMatrixWindow(EditMatrixViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
