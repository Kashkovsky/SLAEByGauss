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
        public void ShowReversePass(double[,] matrix, double x1, double x2, double x3, double x4, double[] E)
        {
            string result = "Reverse pass:\r\n";
            result += $"X4 = {matrix[0, 4]} / {matrix[0, 3]}\r\n" +
                      $"X3 = ({matrix[1, 4]} - (x4 * {matrix[1, 3]})) / {matrix[1, 2]}\r\n" +
                      $"X2 = ({matrix[2, 4]} - (x3 *{matrix[2, 2]}) - (x4 * {matrix[2, 3]})) / {matrix[2, 1]}\r\n" +
                      $"X1 = ({matrix[3, 4]} - (x2 * {matrix[3, 1]}) - (x3 * {matrix[3, 2]}) - (x4 * {matrix[3, 3]})) / {matrix[3, 0]}\r\n";
            result += "Final result: \r\n";
            result += $"X1 = {x1}\r\nX2 = {x2}\r\nX3 = {x3}\r\nX4 = {x4}\r\n";
            result += "\r\nResidual vectors: \r\n";
            for (int i = 0; i < E.Length; i++)
            {
                result += $"E{i+1} = {E[i]}\r\n";
            }
            resultMonitor.Text += result;
        }
        public void IsInconsistent()
        {
            resultMonitor.Text = "The SLAE is inconsistent.";
        }
        
    }
}
