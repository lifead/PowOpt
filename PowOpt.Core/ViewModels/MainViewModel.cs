using PowOpt.Core.Models;
using PowOpt.Core.Repositories;
using PowOpt.Core.Services;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;

namespace PowOpt.Core.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        private readonly IProjectRepository _projectRepository;
        private readonly DataTransformationService _dataTransformationService;

        private ObservableCollection<GroupViewModel> _displayGroups;
        public ObservableCollection<GroupViewModel> DisplayGroups
        {
            get => _displayGroups;
            set => this.RaiseAndSetIfChanged(ref _displayGroups, value);
        }

        public ReactiveCommand<Unit, Unit> OpenProjectCommand { get; }

        public MainViewModel(IProjectRepository projectRepository, DataTransformationService dataTransformationService)
        {
            _projectRepository = projectRepository;
            _dataTransformationService = dataTransformationService;

            DisplayGroups = new ObservableCollection<GroupViewModel>();

            OpenProjectCommand = ReactiveCommand.Create(OpenProject);
        }

        private void OpenProject()
        {
            string filePath = "projectData.json";
            var data = _projectRepository.LoadProject(filePath);

            if (data != null)
            {
                var displayData = _dataTransformationService.TransformToViewModel(data);

                DisplayGroups.Clear();
                foreach (var group in displayData.Groups)
                {
                    DisplayGroups.Add(group);
                }
            }
        }
    }
}
