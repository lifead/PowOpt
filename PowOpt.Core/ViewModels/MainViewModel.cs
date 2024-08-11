using GalaSoft.MvvmLight;
using PowOpt.Core.Repositories;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PowOpt.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IProjectRepository _projectRepository;

        public ObservableCollection<string> Parameters { get; set; }
        public ObservableCollection<string> Formulas { get; set; }
        public ObservableCollection<string> Charts { get; set; }

        public ICommand OpenProjectCommand { get; }

        public MainViewModel(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;

            Parameters = new ObservableCollection<string>();
            Formulas = new ObservableCollection<string>();
            Charts = new ObservableCollection<string>();

            OpenProjectCommand = new RelayCommand(OpenProject);
        }

        private void OpenProject()
        {
            string filePath = "projectData.json";
            var data = _projectRepository.LoadProject(filePath);

            if (data != null)
            {
                Parameters.Clear();
                Formulas.Clear();
                Charts.Clear();

                foreach (var param in data.Parameters)
                {
                    Parameters.Add(param);
                }

                foreach (var formula in data.Formulas)
                {
                    Formulas.Add(formula);
                }

                foreach (var chart in data.Charts)
                {
                    Charts.Add(chart);
                }
            }
        }
    }
}
