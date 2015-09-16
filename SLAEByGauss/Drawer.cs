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
        int colLength;
        int rowLength;

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
        }

        private void Solver_CalculationComplete(object sender, ResultArgs e)
        {
       
            resultMonitor.Clear();
            resultMonitor.Text += e.Message;
        }

        private void Solver_UpdateGridRepresentation(object sender, GridArgs e)
        {
            colLength = e.Matrix.GetLength(0);
            rowLength = e.Matrix.GetLength(1);

                for (int i = 0; i < colLength; i++)
                {
                    double[] rowToAdd = new double[rowLength];
                    for (int j = 0; j < rowLength; j++)
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
       

        public void ShowReversePass(double[,] matrix, double[] X, double[] E)
        {
            int freeMember = matrix.GetLength(1) - 1;
            
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
            
            result += "\r\nResidual vectors: \r\n";
            if (E != null)
            {
                for (int i = 0; i < E.Length; i++)
                {
                    result += $"E{i + 1} = {E[i]}\r\n";
                }
            }
            
            resultMonitor.Text += result;
        }
        public void ShowReversePass(double[,] matrix, double[] X, double[] E, bool multiple)
        {
            int freeMember = matrix.GetLength(1) - 1;
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
                    {
                        result += $" - {X[m]}";
                    }
                    result += $") / {matrix[xRow, xCol]}\r\n";
                }
            }
            result += "Final result: \r\n(";
            foreach (double xn in X)
            {
                result += $"{xn}; ";
            }
            result += ")";
            result += "\r\nResidual vectors: \r\n";
            if (E != null)
            {
                for (int i = 0; i < E.Length; i++)
                {
                    result += $"E{i + 1} = {E[i]}\r\n";
                }
            }
            resultMonitor.Text += result;
        }
        public void IsInconsistent()
        {
            resultMonitor.Text = "The Slae is inconsistent.";
        }
    }
}
