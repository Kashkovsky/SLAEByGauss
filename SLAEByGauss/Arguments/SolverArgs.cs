using System;

namespace SLAEByGauss.Arguments
{
    public class SolverArgs : EventArgs
    {
        public int TargetRow { get; set; }
        public int CopyRow { get; set; }
        public double CopyMultiplier { get; set; }
        public double TargetMultiplier { get; set; }
        readonly private string _sign;
        readonly private string _printedMatrix;


        public string Message { get; private set; }
        public SolverArgs(double[,] matrix, int targetRow, int copyRow, double targetMultiplier, double copyMultiplier, string sign)
        {
            
            this.TargetRow = targetRow;
            this.CopyRow = copyRow;
            this.TargetMultiplier = targetMultiplier;
            this.CopyMultiplier = copyMultiplier;
            this._sign = sign;
            _printedMatrix = PrintMatrix(matrix);
            Message = $"Multiply row {TargetRow} by {TargetMultiplier}\r\n" +
                       $"Multiply row {CopyRow} by " + _sign + $"{CopyMultiplier}. \r\n" +
                       $"Add row {CopyRow} to row {TargetRow}\r\n" +
                       $"{_printedMatrix} \r\n";
        }

        public SolverArgs(double[,] matrix)
        {
            _printedMatrix = PrintMatrix(matrix);
            Message = "Lets create an extended matrix:\r\n" + $"{_printedMatrix}";
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