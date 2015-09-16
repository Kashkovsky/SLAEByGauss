using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SLAEByGauss
{
    class Drawer
    {
        private TextBox monitor;
        private TextBox resultMonitor;
        private DataGridView grid;
        private DataGridView grid2;
        DataTable table;
        DataTable triangularTable;
        string result = "";
        double delta;
        int _colLength;
        int _rowLength;

        public Drawer(TextBox monitor, TextBox resultMonitor, DataGridView grid, DataGridView grid2)
        {
            this.monitor = monitor;
            this.resultMonitor = resultMonitor;
            this.grid = grid;
            this.grid2 = grid2;
            table = new DataTable();
            triangularTable = new DataTable();
            table.Columns.Add("A1", typeof(int));
            table.Columns.Add("A2", typeof(int));
            table.Columns.Add("A3", typeof(int));
            table.Columns.Add("A4", typeof(int));
            table.Columns.Add("C", typeof(int));
            triangularTable.Columns.Add("A1", typeof(int));
            triangularTable.Columns.Add("A2", typeof(int));
            triangularTable.Columns.Add("A3", typeof(int));
            triangularTable.Columns.Add("A4", typeof(int));
            triangularTable.Columns.Add("C", typeof(int));
            grid.DataSource = table;
            grid2.DataSource = triangularTable;
            Solver.UpdateMatrix += SolverUpdateMatrix;
            Solver.UpdateGridRepresentation += Solver_UpdateGridRepresentation;
            Solver.CalculationComplete += Solver_CalculationComplete;
            Solver.ReversePassComplete += Solver_ReversePassComplete;
        }

        private void Solver_ReversePassComplete(object sender, ReversePassArgs e)
        {
            resultMonitor.Text += e.Message;
        }

        private void Solver_CalculationComplete(object sender, ResultArgs e)
        {
       
            resultMonitor.Clear();
            resultMonitor.Text += e.Message;
        }

        private void Solver_UpdateGridRepresentation(object sender, GridArgs e)
        {
            _colLength = e.Matrix.GetLength(0);
            _rowLength = e.Matrix.GetLength(1);

                for (int i = 0; i < _colLength; i++)
                {
                    double[] rowToAdd = new double[_rowLength];
                    for (int j = 0; j < _rowLength; j++)
                    {
                        rowToAdd[j] = e.Matrix[i, j];
                    }
                    if (e.TableNumber == 0) table.Rows.Add(rowToAdd);
                    else triangularTable.Rows.Add(rowToAdd);
                }
          
                SetColWidth();
        }

        private void SolverUpdateMatrix(object sender, SolverArgs e)
        {
            monitor.Text = e.Message;
        }

        private void SetColWidth()
        {
            foreach (DataGridViewColumn column in grid.Columns)
            {
                column.Width = 50;
            }
            foreach (DataGridViewColumn column in grid2.Columns)
            {
                column.Width = 50;
            }
        }

        public void IsInconsistent()
        {
            resultMonitor.Text = "The Slae is inconsistent.";
        }
    }
}
