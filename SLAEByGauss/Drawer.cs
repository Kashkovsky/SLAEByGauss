using System;
using System.Data;
using System.Windows.Forms;
using SLAEByGauss.Arguments;

namespace SLAEByGauss
{
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
            _resultMonitor.Clear();
            _resultMonitor.Text += e.Message;  
        }

        private void Solver_UpdateGridRepresentation(object sender, GridArgs e)
        {
            _colLength = e.Matrix.GetLength(0);
            _rowLength = e.Matrix.GetLength(1);
            
                for (int i = 0; i < _colLength; i++)
                {
                    object[] rowToAdd = new object[_rowLength];
                    
                    
                    for (int j = 0; j < _rowLength; j++)
                    {
                        rowToAdd[j] = e.Matrix[i, j];
                    }

                    if (e.TableNumber == 0)
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
            Solver.UpdateMatrix -= SolverUpdateMatrix;
            Solver.CalculationComplete -= Solver_CalculationComplete;
            Solver.ReversePassComplete -= Solver_ReversePassComplete;
            Solver.UpdateGridRepresentation -= Solver_UpdateGridRepresentation;
            Solver.RowsSwaped -= Solver_RowsSwaped;
        }
    }
}
