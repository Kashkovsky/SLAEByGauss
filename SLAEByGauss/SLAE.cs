using System;
using System.Windows.Forms;

namespace SLAEByGauss
{
    public partial class Slae : Form
    {
        double[,] _matrix;
      //  private Solver _solver;
       // private Drawer _drawer;
        public Slae()
        {
            InitializeComponent();
        }
        private void calculateButton_Click(object sender, EventArgs e)
        {
            _matrix = new double[4,5];
            
            if (double.TryParse(a11.Text, out _matrix[0,0]) &&
                double.TryParse(a12.Text, out _matrix[0,1]) &&
                double.TryParse(a13.Text, out _matrix[0,2]) &&
                double.TryParse(a14.Text, out _matrix[0,3]) &&
                double.TryParse(a15.Text, out _matrix[0,4]) &&
                double.TryParse(a21.Text, out _matrix[1,0]) &&
                double.TryParse(a22.Text, out _matrix[1,1]) &&
                double.TryParse(a23.Text, out _matrix[1,2]) &&
                double.TryParse(a24.Text, out _matrix[1,3]) &&
                double.TryParse(a25.Text, out _matrix[1,4]) &&
                double.TryParse(a31.Text, out _matrix[2,0]) &&
                double.TryParse(a32.Text, out _matrix[2,1]) &&
                double.TryParse(a33.Text, out _matrix[2,2]) &&
                double.TryParse(a34.Text, out _matrix[2,3]) &&
                double.TryParse(a35.Text, out _matrix[2,4]) &&
                double.TryParse(a41.Text, out _matrix[3,0]) &&
                double.TryParse(a42.Text, out _matrix[3,1]) &&
                double.TryParse(a43.Text, out _matrix[3,2]) &&
                double.TryParse(a44.Text, out _matrix[3,3]) &&
                double.TryParse(a45.Text, out _matrix[3,4])
                )
            {

                using(Drawer _drawer = new Drawer(monitor, resultMonitor, dataGridView1, dataGridView2))
                {
                    Solver solver = new Solver(_matrix);
                    solver.SolveEquasion();
                }

                
                
            }
            else
            {
                monitor.Text = "Wrong input. Numeric values expected.";
            }
        }

        private void infiniteButton_Click(object sender, EventArgs e)
        {
            a11.Text = "2"; a12.Text = "3"; a13.Text = "-1"; a14.Text = "1"; a15.Text = "1";
            a21.Text = "8"; a22.Text = "12"; a23.Text = "-9"; a24.Text = "8"; a25.Text = "3";
            a31.Text = "4"; a32.Text = "6"; a33.Text = "3"; a34.Text = "-2"; a35.Text = "3";
            a41.Text = "2"; a42.Text = "3"; a43.Text = "9"; a44.Text = "-7"; a45.Text = "3";
        }

        private void inconsistentButton_Click(object sender, EventArgs e)
        {
            a11.Text = "1"; a12.Text = "1"; a13.Text = "1"; a14.Text = "1"; a15.Text = "4";
            a21.Text = "2"; a22.Text = "2"; a23.Text = "2"; a24.Text = "2"; a25.Text = "8";
            a31.Text = "3"; a32.Text = "3"; a33.Text = "3"; a34.Text = "3"; a35.Text = "13";
            a41.Text = "4"; a42.Text = "4"; a43.Text = "4"; a44.Text = "4"; a45.Text = "16";
        }

        private void taskButton_Click(object sender, EventArgs e)
        {
            a11.Text = "-3"; a12.Text = "4"; a13.Text = "1"; a14.Text = "4"; a15.Text = "-1";
            a21.Text = "0"; a22.Text = "1"; a23.Text = "3"; a24.Text = "2"; a25.Text = "-1";
            a31.Text = "4"; a32.Text = "0"; a33.Text = "-2"; a34.Text = "-3"; a35.Text = "4";
            a41.Text = "1000"; a42.Text = "3"; a43.Text = "1"; a44.Text = "-5"; a45.Text = "-2";
        }
    }
}
