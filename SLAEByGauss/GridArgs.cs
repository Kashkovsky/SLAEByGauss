namespace SLAEByGauss
{
    public class GridArgs
    {
        public double[,] Matrix { get; set; }
        public int TableNumber { get; set; }

        public GridArgs(double[,] matrix, int tableNumber)
        {
            this.Matrix = matrix;
            this.TableNumber = tableNumber;
        }
    }
}