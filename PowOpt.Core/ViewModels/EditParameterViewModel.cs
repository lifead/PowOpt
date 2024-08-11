using PowOpt.Core.Models;
using ReactiveUI;
using System.Reactive;

namespace PowOpt.Core.ViewModels
{
    public class EditParameterViewModel : ReactiveObject
    {
        private string _parameterName;
        public string ParameterName
        {
            get => _parameterName;
            set => this.RaiseAndSetIfChanged(ref _parameterName, value);
        }

        public ReactiveCommand<Unit, Unit> SaveCommand { get; }

        public EditParameterViewModel(ParameterViewModel parameter)
        {
            ParameterName = parameter.ParameterName;

            SaveCommand = ReactiveCommand.Create(Save);
        }

        private void Save()
        {
            // Логика сохранения параметра, например, обновление данных в базе или файле.
        }
    }
}
