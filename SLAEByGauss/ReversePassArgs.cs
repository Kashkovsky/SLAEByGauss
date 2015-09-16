using System.Collections.Generic;

namespace SLAEByGauss
{
    public class ReversePassArgs
    {
        public double[,] Matrix { get; set; }
        public double[] X { get;  set; }
        public double[] E { get;  set; }
        public string Message { get; set; }

        public ReversePassArgs(double[,] matrix, double[] x, double[] e, bool hasMultipleSolutions)
        {
            this.Matrix = matrix;
            this.X = x;
            this.E = e;
            Message = ComposeMessage(hasMultipleSolutions);
        }

        private string ComposeMessage(bool hms)
        {
            int freeMember = Matrix.GetLength(1) - 1;
            string result = "Reverse pass:\r\n";
            int colLength = Matrix.GetLength(0);
            if (hms)
            {
               
                List<int> zeroX = new List<int>();
                for (int i = 0; i < X.Length; i++)
                {
                    if (X[i] == 0)
                    {
                        result += $"Let X{i + 1} = 0\r\n";
                        zeroX.Add(i + 1);
                    }
                }
                int xNumber;
                int xRow;
                int xCol;
                for (int i = X.Length; i > 0; i--)
                {
                    xNumber = i;
                    if (!zeroX.Contains(xNumber))
                    {
                        xRow = colLength - i;
                        xCol = xNumber - 1;
                        result += $"X{xNumber} = ({Matrix[xRow, freeMember]}";
                        for (int m = i; m < freeMember; m++)
                        {
                            result += $" - {X[m]}";
                        }
                        result += $") / {Matrix[xRow, xCol]}\r\n";
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
            }
            //---------------------------------------------------
            else
            {
                

                for (int xRow = 0; xRow < X.Length; xRow++)
                {
                    int xNumber = X.Length - xRow;
                    int xCol = xNumber - 1;
                    result += $"X{xNumber} = ({Matrix[xRow, freeMember]}";
                    for (int j = 0; j < xRow; j++)
                    {
                        int otherXNumber = xNumber + (j + 1);
                        int otherXCol = xNumber + j;
                        result += $" - (x{otherXNumber} * ";
                        if (Matrix[xRow, otherXCol] < 0) result += $"({Matrix[xRow, otherXCol]})";
                        else result += $"{Matrix[xRow, otherXCol]}";
                    }
                    result += $") / {Matrix[xRow, xCol]}\r\n";

                }

                result += "Final result: \r\n";
                for (int i = 0; i < X.Length; i++)
                {
                    result += $"X{i + 1} = {X[i]}\r\n";
                }

                result += "\r\nResidual vectors: \r\n";
                if (E != null)
                {
                    for (int i = 0; i < E.Length; i++)
                    {
                        result += $"E{i + 1} = {E[i]}\r\n";
                    }
                }
            }
            
            return result;
        }
    }
}