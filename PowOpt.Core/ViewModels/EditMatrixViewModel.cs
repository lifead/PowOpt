using PowOpt.Core.Models;
using PowOpt.Core.Repositories;
using ReactiveUI;
using System.Reactive;

namespace PowOpt.Core.ViewModels
{
    public class EditMatrixViewModel : ReactiveObject
    {
        private readonly IProjectRepository _projectRepository;
        private readonly string _filePath;
        private MatrixDataDbo _matrixData;

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

        public EditMatrixViewModel(IProjectRepository projectRepository, string filePath)
        {
            _projectRepository = projectRepository;
            _filePath = filePath;

            // Загрузка данных матрицы
            _matrixData = _projectRepository.LoadMatrixData(_filePath);

            RowCount = _matrixData?.RowCount ?? 0;
            ColumnCount = _matrixData?.ColumnCount ?? 0;

            SaveCommand = ReactiveCommand.Create(Save);
        }

        private void Save()
        {
            _matrixData.RowCount = RowCount;
            _matrixData.ColumnCount = ColumnCount;

            _projectRepository.SaveMatrixData(_filePath, _matrixData);
        }
    }
}
