using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
        public void DrawMatrix(double[,] matrix, byte tableNumber)
        {
            int colLength = matrix.GetLength(0);
            for (int i = 0; i < colLength; i++)
            {
                if (tableNumber == 0) table.Rows.Add(matrix[i, 0], matrix[i, 1], matrix[i, 2], matrix[i, 3], matrix[i, 4]);
                else triangularTable.Rows.Add(matrix[i, 0], matrix[i, 1], matrix[i, 2], matrix[i, 3], matrix[i, 4]);
            }
            
            SetColWidth();
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
        public void ShowDescription(double[,] matrix, string description)
        {
            result += $"\r\n{description}\r\n";
            for (int i = 0; i < 4; i++)
            {
                result += "|";
                for (int j = 0; j < 5; j++)
                {
                    result += $"{matrix[i, j]}\t";
                }
                result += "|\r\n";
            }
            monitor.Text = result;
        }
        public void ShowResult(double delta, bool consistent)
        {
            resultMonitor.Clear();
            resultMonitor.Text += $"Triangular determinant equals {delta}\r\n";
            if (delta == 0) {
                if (consistent) resultMonitor.Text += "The SLAE has an infinite number of solutions.\r\n";
            } 
            else resultMonitor.Text += "The SLAE has a unique solution: \r\n";
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
        public void IsInconsistent()
        {
            resultMonitor.Text = "The SLAE is inconsistent.";
        }
        
    }
}
