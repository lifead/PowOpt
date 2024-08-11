using PowOpt.Core.Models;
using PowOpt.Core.Repositories;
using ReactiveUI;
using System.Reactive;

namespace PowOpt.Core.ViewModels
{
    public class EditMatrixViewModel : ReactiveObject
    {
        private readonly IProjectRepository _projectRepository;
        private MatrixDataDbo _matrixData;

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                LoadMatrixData();
            }
        }

        private int _rowCount;
        public int RowCount
        {
            get => _rowCount;
            set => this.RaiseAndSetIfChanged(ref _rowCount, value);
        }

        private int _columnCount;
        public int ColumnCount
        {
            get => _columnCount;
            set => this.RaiseAndSetIfChanged(ref _columnCount, value);
        }

        public ReactiveCommand<Unit, Unit> SaveCommand { get; }

        public EditMatrixViewModel(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
            SaveCommand = ReactiveCommand.Create(Save);
        }

        private void LoadMatrixData()
        {
            _matrixData = _projectRepository.LoadMatrixData(FilePath);
            RowCount = _matrixData?.RowCount ?? 0;
            ColumnCount = _matrixData?.ColumnCount ?? 0;
        }

        private void Save()
        {
            _matrixData.RowCount = RowCount;
            _matrixData.ColumnCount = ColumnCount;

            _projectRepository.SaveMatrixData(FilePath, _matrixData);
        }
    }
}
