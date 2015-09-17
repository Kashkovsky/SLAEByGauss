using System;
using System.Data;
using System.Windows.Forms;
using SLAEByGauss.Arguments;

namespace SLAEByGauss
{
<<<<<<< HEAD
    public class Drawer : IDisposable
    {
        private readonly TextBox _monitor;
        private readonly TextBox _resultMonitor;
        private readonly DataGridView _grid;
        private readonly DataGridView _grid2;
        private readonly DataTable _table;
        private readonly DataTable _triangularTable;
        private int _colLength;
        private int _rowLength;
=======
    public class Drawer
    {
        private TextBox monitor;
        private TextBox resultMonitor;
        private DataGridView grid;
        private DataGridView grid2;
        DataTable table;
        DataTable triangularTable;
        string result = "";
        int colLength;
        int rowLength;

>>>>>>> master
        public Drawer(TextBox monitor, TextBox resultMonitor, DataGridView grid, DataGridView grid2)
        {
            this._monitor = monitor;
            this._resultMonitor = resultMonitor;
            this._grid = grid;
            this._grid2 = grid2;
            _table = new DataTable();
            _triangularTable = new DataTable();
            _table.Columns.Add("A1", typeof(int));
            _table.Columns.Add("A2", typeof(int));
            _table.Columns.Add("A3", typeof(int));
            _table.Columns.Add("A4", typeof(int));
            _table.Columns.Add("C", typeof(int));
            _triangularTable.Columns.Add("A1", typeof(int));
            _triangularTable.Columns.Add("A2", typeof(int));
            _triangularTable.Columns.Add("A3", typeof(int));
            _triangularTable.Columns.Add("A4", typeof(int));
            _triangularTable.Columns.Add("C", typeof(int));
            grid.DataSource = _table;
            grid2.DataSource = _triangularTable;
            _monitor.Clear();
            _resultMonitor.Clear();
         
            Solver.UpdateMatrix += SolverUpdateMatrix;
            Solver.UpdateGridRepresentation += Solver_UpdateGridRepresentation;
            Solver.CalculationComplete += Solver_CalculationComplete;
            Solver.ReversePassComplete += Solver_ReversePassComplete;
            Solver.RowsSwaped += Solver_RowsSwaped;
        }

        private void Solver_RowsSwaped(object sender, SwapRowsArgs e)
        {
            _monitor.Text += e.Message;
        }

        private void Solver_ReversePassComplete(object sender, ReversePassArgs e)
        {
            _resultMonitor.Text += e.Message;
        }

        private void Solver_CalculationComplete(object sender, ResultArgs e)
        {
<<<<<<< HEAD
            _resultMonitor.Clear();
            _resultMonitor.Text += e.Message;  
=======
            resultMonitor.Clear();
            resultMonitor.Text += $"Triangular determinant equals {delta}\r\n";
            if (delta == 0) {
                if (consistent) resultMonitor.Text += "The SLAE has an infinite number of solutions.\r\n";
                else IsInconsistent();
            } 
            else resultMonitor.Text += "The SLAE has a unique solution: \r\n";
>>>>>>> master
        }

        private void Solver_UpdateGridRepresentation(object sender, GridArgs e)
        {
<<<<<<< HEAD
            _colLength = e.Matrix.GetLength(0);
            _rowLength = e.Matrix.GetLength(1);
=======
            int freeMember = rowLength - 1;
            
            string result = "Reverse pass:\r\n";
            for (int xRow = 0; xRow < X.Length; xRow++) 
            {
                int xNumber = X.Length - xRow;
                int xCol = xNumber - 1;                         
                result += $"X{xNumber} = ({matrix[xRow, freeMember]}"; 
                for (int j = 0; j < xRow; j++) 
                {   
                    int otherXNumber = xNumber + (j + 1);
                    int otherXCol = xNumber + j;
                    result += $" - (x{otherXNumber} * ";
                    if (matrix[xRow, otherXCol] < 0) result += $"({matrix[xRow, otherXCol]})";
                    else result += $"{matrix[xRow, otherXCol]}";
                }
                result += $") / {matrix[xRow, xCol]}\r\n";
                   
            }
          
            result += "Final result: \r\n";
            for (int i = 0; i < X.Length; i++)
            {
                result += $"X{i+1} = {X[i]}\r\n";
            }
>>>>>>> master
            
                for (int i = 0; i < _colLength; i++)
                {
<<<<<<< HEAD
                    object[] rowToAdd = new object[_rowLength];
                    
                    
                    for (int j = 0; j < _rowLength; j++)
                    {
                        rowToAdd[j] = e.Matrix[i, j];
                    }

                    if (e.TableNumber == 0)
=======
                    result += $"E{i + 1} = {E[i]}\r\n";
                }
            }
            
            resultMonitor.Text += result;
        }
        public void ShowReversePass(double[,] matrix, double[] X, double[] E, bool multiple)
        {
            int freeMember = rowLength - 1;
            List<int> zeroX = new List<int>();
            string result = "";
            for (int i = 0; i < X.Length; i++)
            {
                if (X[i] == 0)
                {
                    result += $"Let X{i + 1} = 0\r\n";
                    zeroX.Add(i+1);
                } 
            }
            int xNumber;
            int xRow;
            int xCol;
            for (int i = X.Length; i > 0; i--) 
            {
                xNumber = i;
                if (!zeroX.Contains(xNumber)) {
                    xRow = colLength - i;
                    xCol = xNumber - 1;
                    result += $"X{xNumber} = ({matrix[xRow, freeMember]}";
                    for (int m = i; m < freeMember; m++)
>>>>>>> master
                    {
                        DataRow row = _table.NewRow();
                        row.ItemArray = rowToAdd;
                        _table.Rows.Add(row);
                    }
                    else
                    {
                        DataRow row = _triangularTable.NewRow();
                        row.ItemArray = rowToAdd;
                        _triangularTable.Rows.Add(row);
                    }
                }
          
                SetColWidth();

        }

        private void SolverUpdateMatrix(object sender, SolverArgs e)
        {
            _monitor.Text += e.Message;
        }

        private void SetColWidth()
        {
            foreach (DataGridViewColumn column in _grid.Columns)
            {
                column.Width = 50;
            }
            foreach (DataGridViewColumn column in _grid2.Columns)
            {
                column.Width = 50;
            }
        }

        public void Dispose()
        {
<<<<<<< HEAD
            Solver.UpdateMatrix -= SolverUpdateMatrix;
            Solver.CalculationComplete -= Solver_CalculationComplete;
            Solver.ReversePassComplete -= Solver_ReversePassComplete;
            Solver.UpdateGridRepresentation -= Solver_UpdateGridRepresentation;
            Solver.RowsSwaped -= Solver_RowsSwaped;
=======
            resultMonitor.Text += "The SLAE is inconsistent.";
>>>>>>> master
        }
    }
}
