using PowOpt.Core.Models;
using PowOpt.Core.Repositories;
using System.Collections.ObjectModel;

namespace PowOpt.Core.ViewModels
{
    public class EditMatrixViewModel : BindableBase
    {
        private readonly IProjectRepository _projectRepository;
        private MatrixDataDbo _matrixData;
        private RectangleInfo _selectedRectangle; // Для отслеживания предыдущего выбранного прямоугольника

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set
            {
                SetProperty(ref _filePath, value);
                LoadMatrixData();
            }
        }

        private int _rowCount;
        public int RowCount
        {
            get => _rowCount;
            set
            {
                SetProperty(ref _rowCount, value);
                UpdateRectangleSize();
                UpdateFragments();
            }
        }

        private int _columnCount;
        public int ColumnCount
        {
            get => _columnCount;
            set
            {
                SetProperty(ref _columnCount, value);
                UpdateRectangleSize();
                UpdateFragments();
            }
        }

        private double _rectangleWidth;
        public double RectangleWidth
        {
            get => _rectangleWidth;
            private set => SetProperty(ref _rectangleWidth, value);
        }

        private double _rectangleHeight;
        public double RectangleHeight
        {
            get => _rectangleHeight;
            private set => SetProperty(ref _rectangleHeight, value);
        }

        public ObservableCollection<MatrixBlockDbo> MatrixBlocks { get; set; } = new ObservableCollection<MatrixBlockDbo>();

        // Свойство для хранения обработанного массива Values
        private ObservableCollection<ObservableCollection<string>> _filteredValues;
        public ObservableCollection<ObservableCollection<string>> FilteredValues
        {
            get => _filteredValues;
            set => SetProperty(ref _filteredValues, value);
        }

        private MatrixBlockDbo _selectedMatrixBlock;
        public MatrixBlockDbo SelectedMatrixBlock
        {
            get => _selectedMatrixBlock;
            set
            {
                SetProperty(ref _selectedMatrixBlock, value);
                UpdateRectangleColor(value);
                UpdateFilteredValues(value?.Values);
            }
        }

        // Коллекция для хранения данных о вложенных прямоугольниках
        public ObservableCollection<RectangleInfo> Rectangles { get; set; } = new ObservableCollection<RectangleInfo>();

        public DelegateCommand SaveCommand { get; }

        public EditMatrixViewModel(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
            SaveCommand = new DelegateCommand(Save);
        }

        private void UpdateFilteredValues(string[][] values)
        {
            if (values == null || values.Length == 0)
            {
                FilteredValues = new ObservableCollection<ObservableCollection<string>>();
                return;
            }

            // Заполняем пропущенные значения пустыми строками, если необходимо
            var filtered = values
                .Where(row => row.Any(cell => !string.IsNullOrWhiteSpace(cell)))
                .Select(row => row.Concat(Enumerable.Repeat(string.Empty, Math.Max(0, 3 - row.Length))) // Делаем строку длиной не меньше 3
                                  .ToObservableCollection())
                .ToObservableCollection();

            FilteredValues = filtered;
        }

        public void LoadMatrixData()
        {
            _matrixData = _projectRepository.LoadMatrixData(FilePath);
            RowCount = _matrixData?.RowCount ?? 0;
            ColumnCount = _matrixData?.ColumnCount ?? 0;

            MatrixBlocks.Clear();
            if (_matrixData?.MatrixBlocks != null)
            {
                foreach (var block in _matrixData.MatrixBlocks)
                {
                    MatrixBlocks.Add(block);
                }
            }

            UpdateFragments();
        }

        private void UpdateRectangleSize()
        {
            RectangleWidth = RowCount * 40;
            RectangleHeight = ColumnCount * 40;
        }

        private void UpdateFragments()
        {
            Rectangles.Clear();
            int zIndex = 0;
            foreach (var block in MatrixBlocks)
            {
                double startX = block.StartFragmentX * 40;
                double startY = block.StartFragmentY * 40;
                double endX = (block.EndFragmentX + 1) * 40;
                double endY = (block.EndFragmentY + 1) * 40;

                Rectangles.Add(new RectangleInfo(startX, startY, endX - startX, endY - startY, block.FragmentName, zIndex));
                zIndex++;
            }
        }

        private void UpdateRectangleColor(MatrixBlockDbo selectedBlock)
        {
            if (_selectedRectangle != null)
            {
                // Возвращаем предыдущий выбранный блок к исходному состоянию
                _selectedRectangle.Color = "Transparent";
            }

            // Находим текущий выбранный прямоугольник и меняем его цвет на зеленый
            var rectangle = Rectangles.FirstOrDefault(r => r.FragmentName == selectedBlock?.FragmentName);
            if (rectangle != null)
            {
                rectangle.Color = "Green";
                _selectedRectangle = rectangle; // Сохраняем ссылку на текущий выбранный прямоугольник
            }
        }

        private void Save()
        {
            _matrixData.RowCount = RowCount;
            _matrixData.ColumnCount = ColumnCount;
            _matrixData.MatrixBlocks = new List<MatrixBlockDbo>(MatrixBlocks);

            _projectRepository.SaveMatrixData(FilePath, _matrixData);
        }
    }

    // Вспомогательный метод расширения для преобразования в ObservableCollection
    public static class Extensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
        {
            return new ObservableCollection<T>(enumerable);
        }
    }
}
