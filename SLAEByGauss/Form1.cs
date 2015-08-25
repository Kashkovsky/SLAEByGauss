using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SLAEByGauss
{
    public partial class Form1 : Form
    {
        int[,] matrix;
        Solver solver;
        public Form1()
        {
            InitializeComponent();
        }
        private void calculateButton_Click(object sender, EventArgs e)
        {
            matrix = new int[4,5];
            
            if (int.TryParse(a11.Text, out matrix[0,0]) &&
                int.TryParse(a12.Text, out matrix[0,1]) &&
                int.TryParse(a13.Text, out matrix[0,2]) &&
                int.TryParse(a14.Text, out matrix[0,3]) &&
                int.TryParse(a15.Text, out matrix[0,4]) &&
                int.TryParse(a21.Text, out matrix[1,0]) &&
                int.TryParse(a22.Text, out matrix[1,1]) &&
                int.TryParse(a23.Text, out matrix[1,2]) &&
                int.TryParse(a24.Text, out matrix[1,3]) &&
                int.TryParse(a25.Text, out matrix[1,4]) &&
                int.TryParse(a31.Text, out matrix[2,0]) &&
                int.TryParse(a32.Text, out matrix[2,1]) &&
                int.TryParse(a33.Text, out matrix[2,2]) &&
                int.TryParse(a34.Text, out matrix[2,3]) &&
                int.TryParse(a35.Text, out matrix[2,4]) &&
                int.TryParse(a41.Text, out matrix[3,0]) &&
                int.TryParse(a42.Text, out matrix[3,1]) &&
                int.TryParse(a43.Text, out matrix[3,2]) &&
                int.TryParse(a44.Text, out matrix[3,3]) &&
                int.TryParse(a45.Text, out matrix[3,4])
                )
            {
                solver = new Solver(matrix, monitor, resultMonitor, dataGridView1, dataGridView2);
            }
            else
            {
                monitor.Text = "Wrong input. Integer values expected.";
            }
        }

        
    }
}
