using System;
using System.Collections.Generic;
using System.Linq;
using SLAEByGauss.Arguments;

namespace SLAEByGauss
{
    public class Solver
    {
<<<<<<< HEAD
        public double[,] Matrix { get; private set; }
        public double[,] Intact { get; private set; }
        readonly int _rowLength;
        readonly int _colLength;
        public double[] X { get; private set; }
        public double[] E { get; private set; }
        double _determinant;
        private bool HasMultipleOrInconsistent => _determinant == 0;
        readonly int _freeMember;
        readonly int _numberOfbasisVars;

        public Solver(double[,] matrix)
        {
            this.Matrix = matrix;
            this.Intact = matrix;
            _colLength = matrix.GetLength(0);
            _rowLength = matrix.GetLength(1);
            _freeMember = _rowLength - 1;
            X = new double[_rowLength - 1];
            if (_colLength % 2 == 0) _numberOfbasisVars = _colLength / 2;
            else _numberOfbasisVars = (_colLength - 1) / 2;
        }

        public static event EventHandler<SolverArgs> UpdateMatrix;
        public static event EventHandler<SwapRowsArgs> RowsSwaped;
        public static event EventHandler<GridArgs> UpdateGridRepresentation;
        public static event EventHandler<ResultArgs> CalculationComplete;
        public static event EventHandler<ReversePassArgs> ReversePassComplete;   
        
        public void SolveEquasion()
=======
        double[,] matrix;
        double[,] intact;
        int rowLength;
        int colLength;
        double[] X;
        double[] E;
        double determinant = 0;
        int freeMember;
        int numberOfbasisVars;
        bool consistent = true;
        Drawer drawer;

        public Solver(double[,] matrix) {
            this.matrix = matrix;
            this.intact = matrix;
            Setup();
            SolveEquation();
        }
        public Solver(double[,] matrix, TextBox monitor, TextBox resultMonitor, DataGridView grid, DataGridView grid2)
        {
            this.matrix = matrix;
            this.intact = matrix;
            drawer = new Drawer(monitor, resultMonitor, grid, grid2);
            Setup();
            SolveEquation();
        }
        private void Setup() {
            colLength = matrix.GetLength(0);
            rowLength = matrix.GetLength(1);
            freeMember = rowLength - 1;
            X = new double[rowLength - 1];
            if (colLength % 2 == 0) numberOfbasisVars = colLength / 2;
            else numberOfbasisVars = (colLength - 1) / 2;
        }
        public void SolveEquation()
>>>>>>> master
        {
            UpdateGridRepresentation(this, new GridArgs(Matrix, 0));
            UpdateMatrix(this, new SolverArgs(Matrix));
            SwapRows();
            SimpleConversion();
            UpdateGridRepresentation(this, new GridArgs(Matrix, 1));
            CalculateDelta();
<<<<<<< HEAD
            CalculationComplete(this, new ResultArgs(_determinant, CheckConsistency()));
            if (CheckConsistency())
            {
                ReversePass();
                ReversePassComplete(this, new ReversePassArgs(Matrix, X, E, HasMultipleOrInconsistent));

            }
        }
        public bool CheckConsistency()
        {
            for (int i = 0; i < _colLength; i++)
            {
                double leftSum = 0;
                for (int j = 0; j < _rowLength - 1; j++)
                {
                    leftSum += Matrix[i, j];
                }
                if (leftSum == 0 && leftSum != Matrix[i, 4])
                {
                    return false;
=======
            drawer.ShowResult(determinant, consistent);
            FinalResult();
        }
        private void SwapRows()
        {
            for (int rowToCheck = 1; rowToCheck < colLength; rowToCheck++)
            {
                bool workMade = false;
                double temp;
                if (matrix[rowToCheck, 0] == 0)
                {
                    int currentRow = rowToCheck;
                    int previousRow = currentRow - 1;
                    while (matrix[previousRow, 0] != 0)
                    {
                        for (int j = 0; j < rowLength; j++)
                        {
                            temp = matrix[currentRow, j];
                            matrix[currentRow, j] = matrix[previousRow, j];
                            matrix[previousRow, j] = temp;
                        }
                        currentRow--;
                        if (currentRow != 0) previousRow = currentRow - 1;
                        workMade = true;
                    }
>>>>>>> master
                }
                if (workMade) drawer.ShowDescription(matrix, "Swap rows for easy calculations");
            }
        }
        public void SimpleConversion()
        {
            int currentRow = 1;
<<<<<<< HEAD
            int numberOfPasses = 3;
            int colPosition = 0;
            bool consistent = true;
            for (int pass = 0; pass < _colLength - 1; pass++)
=======
            int NumberOfPasses = colLength - 1;
            int colPosition = 0;
            for (int pass = 0; pass < colLength - 1; pass++)
>>>>>>> master
            {
                int copyRow = 0;
                int targetRow = 0;
                double copyRowMultiplier = 0;
                double targetRowMultiplier = 0;
                string sign = "";
                for (int rowNumber = currentRow; rowNumber < numberOfPasses; rowNumber++)
                {
                    int x = (int)Math.Abs(Matrix[rowNumber, colPosition]);
                    int y = (int)Math.Abs(Matrix[rowNumber + 1, colPosition]);
                    double xRaw = Matrix[rowNumber, colPosition];
                    double D = Gcd(x, y);
                    if (x != 0)
                    {
                        for (int i = 0; i < _rowLength; i++)
                        {
                            if (consistent)
                            {
                                double temp = Matrix[rowNumber + 1, i];
                                if (temp < 0) sign = "-";
                                else sign = "";
                                if (xRaw < 0 && Matrix[rowNumber + 1, colPosition] < 0) temp = -temp;
                                if (xRaw > 0 && Matrix[rowNumber + 1, colPosition] > 0) temp = -temp;
                                if (D > 1)
                                {
                                    copyRowMultiplier = x / D;
                                    targetRowMultiplier = y / D;
                                    Matrix[rowNumber, i] *= targetRowMultiplier;
                                    temp *= copyRowMultiplier;
                                }
                                else
                                {
                                    copyRowMultiplier = x;
                                    targetRowMultiplier = y;
                                    Matrix[rowNumber, i] *= targetRowMultiplier;
                                    temp *= copyRowMultiplier;
                                }
                                Matrix[rowNumber, i] += temp;
                            }
                            else return;
                        }
                    }
<<<<<<< HEAD
                    targetRow = rowNumber + 1;
                    copyRow = rowNumber + 2;
                    UpdateMatrix(this, new SolverArgs(Matrix, targetRow, copyRow, targetRowMultiplier, copyRowMultiplier, sign));
=======
                    description = $"Multiply row {rowNumber + 1} by {targetRowMultiplier}\r\n";
                    description += $"Multiply row {rowNumber + 2} by " + sign + $"{copyRowMultiplier}. \r\n";
                    description += $"Add row {rowNumber + 2} to row {rowNumber + 1}";
                    drawer.ShowDescription(matrix, description);
                    CheckConsistency(out consistent);
>>>>>>> master
                }

                if (currentRow != 0) currentRow--;
                numberOfPasses--;
                colPosition++;

            }
        }
<<<<<<< HEAD
        public void SwapRows()
        {
            for (int rowToCheck = 1; rowToCheck < _colLength; rowToCheck++)
            {
                bool workMade = false;
                double temp;
                if (Matrix[rowToCheck, 0] == 0)
                {
                    int currentRow = rowToCheck;
                    int previousRow = currentRow - 1;
                    while (Matrix[previousRow,0] != 0)
                    {
                        for (int j = 0; j < _rowLength; j++)
                        {
                            temp = Matrix[currentRow, j];
                            Matrix[currentRow, j] = Matrix[previousRow, j];
                            Matrix[previousRow, j] = temp;
                        }
                        currentRow--;
                        if (currentRow != 0) previousRow = currentRow - 1;
                        workMade = true;
                    }
                } 
            if(workMade) RowsSwaped(this, new SwapRowsArgs(Matrix));
            }
=======
        private void CheckConsistency(out bool consistent)
        {
            for (int i = 0; i < colLength; i++)
            {
                double leftSum = 0;
                for (int j = 0; j < rowLength - 1; j++)
                {
                    leftSum += matrix[i, j];
                }
                if (leftSum == 0 && leftSum != matrix[i, 4])
                {
                    consistent = false;
                    return;
                }
            };
            consistent = true;
>>>>>>> master
        }
        public void CalculateDelta()
        {
<<<<<<< HEAD
            _determinant = Matrix[3, 0] * Matrix[2, 1] * Matrix[1, 2] * Matrix[0, 3]; 
=======
            determinant = matrix[colLength - 1, 0];
            int j = 1;
            for (int i = colLength - 2; i >= 0; i--)
            {
                determinant *= matrix[i, j];
                j++;
            }
            //determinant = matrix[3, 0] * matrix[2, 1] * matrix[1, 2] * matrix[0, 3]; 
        }
        private void FinalResult()
        {
            if (consistent)
            {
                ReversePass();
                CalculateResidual();
                if (determinant != 0) drawer.ShowReversePass(matrix, X, E);
                else drawer.ShowReversePass(matrix, X, E, true);
            }
>>>>>>> master
        }
        public int Gcd(int x, int y)
        {
            return y == 0 ? x : Gcd(y, x % y);
        }
<<<<<<< HEAD
        public void ReversePass()
=======
        private void ReversePass()        
>>>>>>> master
        {
            if (_determinant > 0 || _determinant < 0)
            {
<<<<<<< HEAD
                for (int xRow = 0; xRow < X.Length; xRow++)
                {
                    int xNumber = X.Length - xRow - 1;      
                    int xCol = xNumber;                         
                    X[xNumber] = (Matrix[xRow, _freeMember]);
                    for (int j = 0; j < xRow; j++)
                    {
                        int previousXNumber = xNumber + (j + 1); 
                        int previousXCol = xNumber + j + 1; 
                        X[xNumber] -= X[previousXNumber] * Matrix[xRow, previousXCol];
                    }
                    X[xNumber] /= Matrix[xRow, xCol];
                }
=======
                // calculate X1, X2, X3, X4 using recursive method RecX(0);
                RecX(0);
                // ... nested iterations also do their job though:
                //for (int xRow = 0; xRow < X.Length; xRow++)
                //{
                //    int xNumber = X.Length - xRow - 1;      
                //    int xCol = xNumber;                         
                //    X[xNumber] = (matrix[xRow, freeMember]);
                //    for (int j = 0; j < xRow; j++)
                //    {
                //        int previousXNumber = xNumber + (j + 1); 
                //        int previousXCol = xNumber + j + 1; 
                //        X[xNumber] -= X[previousXNumber] * matrix[xRow, previousXCol];
                //    }
                //    X[xNumber] /= matrix[xRow, xCol];
                //}
>>>>>>> master
            }
            //---------------------------------------------
            // if _determinant = 0 and Slae has an infinity number of solutions
            else
            {
                SolveMultipleSolutions();
            }
<<<<<<< HEAD
            E = new double[_colLength];
            for (int i = 0; i < _colLength; i++)
=======
        }
        private double RecX(int n)
        {
            int r = colLength - (n + 1);
            X[n] = matrix[r, freeMember];
            for (int i = n + 1; i < colLength; i++)
            {
                X[n] -= RecX(i) * matrix[r, i];
            }
            X[n] /= matrix[r, n];
            return X[n];
        }
        private void CalculateResidual()
        {
            E = new double[colLength];
            for (int i = 0; i < colLength; i++)
>>>>>>> master
            {
                E[i] = Intact[i, _freeMember];
                for (int j = 0; j < X.Length; j++)
                {
                    E[i] -= (X[j] * Intact[i, j]);
                }
            }
        }
        public Dictionary<int, int[]> CheckBasis()
        {
            
            Dictionary<int, int[]> basisDict = new Dictionary<int, int[]>();
            int similarRowNumber = -1;
            for (int i = 0; i < _numberOfbasisVars; i++)
            {
                for (int row = 0; row < _colLength; row++)
                {
                    double[] currentRow = new double[_rowLength];
                    if (row != similarRowNumber)
                    {
                        for (int c = 0; c < _rowLength; c++)
                        {
                            currentRow[c] = Matrix[row, c];
                        }
                        for (int j = 0; j < _rowLength - 1; j++)
                        {
                            if (Matrix[row, j] < 0 || Matrix[row, j] > 0)
                            {   
                                if (!basisDict.Keys.Contains(j)) basisDict.Add(j, new int[] { row, j });
                                break;
                            }
                        }
                    };
                    similarRowNumber = CheckSimilarRow(currentRow, row);
                }
            }
            return basisDict;
        }
        public void SolveMultipleSolutions ()
        {
            Dictionary<int, int[]> basisCoords = CheckBasis();

            for (int col = _colLength; col > 0; col--)
            {
                foreach (var x in basisCoords)
                {
                    X[x.Key] = Matrix[x.Value[0], _freeMember];
                    for (int i = 1; i < _colLength - x.Value[1]; i++)
                    {
                        int xToMinus = x.Value[1] + i;
                        if (basisCoords.Keys.Contains(xToMinus))
                        {
                            X[x.Key] -= X[xToMinus] * Matrix[x.Value[0], xToMinus];
                        } else X[xToMinus] = 0;
                    }
                    X[x.Key] /= Matrix[x.Value[0], x.Value[1]];
                }
            }

        }
        public int CheckSimilarRow(double[] currentRow, int row)
        {
            double[] temp;
            int similarRowNumber = 0;
            for (int i = 0; i < _colLength; i++)
            {
                if (i != row)
                {
                    temp = new double[currentRow.Length];
                    for (int j = 0; j < _rowLength; j++)
                    {
                        if (currentRow[j] < Matrix[i, j] || currentRow[j] > Matrix[i, j])
                        {
                            temp = null;
                            break;
                        }
                        temp[j] = currentRow[j];
                    }
                    if (temp != null)
                    {
                        similarRowNumber = i;
                        return similarRowNumber;
                    };
                }
            }
            return similarRowNumber;
        } 
    }
}
