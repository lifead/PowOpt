using PowOpt.Core.Models;
using PowOpt.Core.Repositories;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;

namespace PowOpt.Core.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        private readonly IProjectRepository _projectRepository;

        private ObservableCollection<Group> _groups;
        public ObservableCollection<Group> Groups
        {
            get => _groups;
            set => this.RaiseAndSetIfChanged(ref _groups, value);
        }

        public ReactiveCommand<Unit, Unit> OpenProjectCommand { get; }

        public MainViewModel(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;

            Groups = new ObservableCollection<Group>();

            OpenProjectCommand = ReactiveCommand.Create(OpenProject);
        }

        private void OpenProject()
        {
            string filePath = "projectData.json";
            var data = _projectRepository.LoadProject(filePath);

            if (data != null)
            {
                Groups.Clear();

                foreach (var group in data.Groups)
                {
                    Groups.Add(group);
                }
            }
        }
    }
}
