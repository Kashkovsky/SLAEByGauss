namespace SLAEByGauss
{
    public class ResultArgs
    {
        public double Determinant { get; set; }
        public bool Consistent { get; set; }
        public string Message {
            get
            {
                string result = $"Triangular determinant equals {Determinant}\r\n";
                if (Determinant == 0)
                {
                    if (Consistent) result += "The Slae has an infinite number of solutions.\r\n";
                }
                else result += "The Slae has a unique solution: \r\n";
                return result;
            }
        }

        public ResultArgs(double determinant, bool consistent)
        {
            this.Determinant = determinant;
            this.Consistent = consistent;
        }
    }
}