namespace SLAEByGauss.Arguments
{
    public class ResultArgs
    {
        public double Determinant { get; set; }
        public bool Consistent { get; set; }
        public string Message {
            get
            {
                string result = $"Triangular determinant equals {Determinant}\r\n";
               
                    if (Consistent)
                    {
                        if (Determinant == 0) result += "The SLAE has an infinite number of solutions.\r\n";
                        else result += "The Slae has a unique solution: \r\n";
                    }
                    else result += "The SLAE is inconsistent.";
                 
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