namespace SLAEByGauss
{
    public class SwapRowsArgs
    {
        public double[,] Matrix { get; set; }
        public string Message => $"Lets swap rows for easy calculations \r\n{PrintMatrix(Matrix)}";

        public SwapRowsArgs(double[,] matrix)
        {
            this.Matrix = matrix;
        }

        private string PrintMatrix(double[,] matrix)
        {
            string result = "";
            for (int i = 0; i < 4; i++)
            {
                result += "| ";
                for (int j = 0; j < 5; j++)
                {
                    result += $"{matrix[i, j]}\t";
                }
                result += "|\r\n";
            }
            return result;
        }
    }
}