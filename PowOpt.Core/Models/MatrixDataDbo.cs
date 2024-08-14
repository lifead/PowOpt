namespace PowOpt.Core.Models
{
    public class MatrixDataDbo
    {
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }
        public List<MatrixBlockDbo> MatrixBlocks { get; set; } // Переименованное поле
    }


    public class MatrixBlockDbo
    {
        public string FragmentName { get; set; }
        public string TypeFragment { get; set; }
        public int StartFragmentX { get; set; }
        public int StartFragmentY { get; set; }
        public int EndFragmentX { get; set; }
        public int EndFragmentY { get; set; }
        public string FormulaValues { get; set; }
        public bool IsDiagonalMatrix { get; set; }
        public string[][] Values { get; set; } // Массив строковых значений внутри блока
    }
}