using PowOpt.Core.Repositories;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;

namespace PowOpt.Core.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        private readonly IProjectRepository _projectRepository;

        private ObservableCollection<string> _parameters;
        public ObservableCollection<string> Parameters
        {
            get => _parameters;
            set => this.RaiseAndSetIfChanged(ref _parameters, value);
        }

        private ObservableCollection<string> _formulas;
        public ObservableCollection<string> Formulas
        {
            get => _formulas;
            set => this.RaiseAndSetIfChanged(ref _formulas, value);
        }

        private ObservableCollection<string> _charts;
        public ObservableCollection<string> Charts
        {
            get => _charts;
            set => this.RaiseAndSetIfChanged(ref _charts, value);
        }

        public ReactiveCommand<Unit, Unit> OpenProjectCommand { get; }

        public MainViewModel(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;

            Parameters = new ObservableCollection<string>();
            Formulas = new ObservableCollection<string>();
            Charts = new ObservableCollection<string>();

            OpenProjectCommand = ReactiveCommand.Create(OpenProject);
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
