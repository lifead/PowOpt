using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows.Input;
using PowOpt.Core.Models;

namespace PowOpt.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<string> Parameters { get; set; }
        public ObservableCollection<string> Formulas { get; set; }
        public ObservableCollection<string> Charts { get; set; }

        public ICommand OpenProjectCommand { get; }

        public MainViewModel()
        {
            Parameters = new ObservableCollection<string>();
            Formulas = new ObservableCollection<string>();
            Charts = new ObservableCollection<string>();

            OpenProjectCommand = new RelayCommand(OpenProject);
        }

        private void OpenProject()
        {
            // Здесь вы можете реализовать открытие файла через диалоговое окно, но для простоты мы используем фиксированный путь
            string filePath = "projectData.json";

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var data = JsonSerializer.Deserialize<ProjectData>(json);

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
