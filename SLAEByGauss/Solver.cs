using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data;

namespace SLAEByGauss
{
    public class Solver
    {
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
        {
            drawer.DrawMatrix(matrix, 0);
            drawer.ShowDescription(matrix, "Create extended matrix");
            SwapRows();
            SimpleConversion();
            drawer.DrawMatrix(matrix, 1);
            CalculateDelta();
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
                }
                if (workMade) drawer.ShowDescription(matrix, "Swap rows for easy calculations");
            }
        }
        private void SimpleConversion()
        {
            int currentRow = 1;
            int NumberOfPasses = colLength - 1;
            int colPosition = 0;
            for (int pass = 0; pass < colLength - 1; pass++)
            {
                double copyRowMultiplier = 0;
                double targetRowMultiplier = 0;
                string description = "";
                string sign = "";
                for (int rowNumber = currentRow; rowNumber < NumberOfPasses; rowNumber++)
                {
                    int x = (int)Math.Abs(matrix[rowNumber, colPosition]);
                    int y = (int)Math.Abs(matrix[rowNumber + 1, colPosition]);
                    double xRaw = matrix[rowNumber, colPosition];
                    double D = GCD(x, y);
                    if (x != 0)
                    {
                        for (int i = 0; i < rowLength; i++)
                        {
                            if (consistent)
                            {
                                double temp = matrix[rowNumber + 1, i];
                                if (temp < 0) sign = "-";
                                else sign = "";
                                if (xRaw < 0 && matrix[rowNumber + 1, colPosition] < 0) temp = -temp;
                                if (xRaw > 0 && matrix[rowNumber + 1, colPosition] > 0) temp = -temp;
                                if (D > 1)
                                {
                                    copyRowMultiplier = x / D;
                                    targetRowMultiplier = y / D;
                                    matrix[rowNumber, i] *= targetRowMultiplier;
                                    temp *= copyRowMultiplier;
                                }
                                else
                                {
                                    copyRowMultiplier = x;
                                    targetRowMultiplier = y;
                                    matrix[rowNumber, i] *= targetRowMultiplier;
                                    temp *= copyRowMultiplier;
                                }
                                matrix[rowNumber, i] += temp;
                            }
                            else return;
                        }
                    }
                    description = $"Multiply row {rowNumber + 1} by {targetRowMultiplier}\r\n";
                    description += $"Multiply row {rowNumber + 2} by " + sign + $"{copyRowMultiplier}. \r\n";
                    description += $"Add row {rowNumber + 2} to row {rowNumber + 1}";
                    drawer.ShowDescription(matrix, description);
                    CheckConsistency(out consistent);
                }

                if (currentRow != 0) currentRow--;
                NumberOfPasses--;
                colPosition++;

            }
        }
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
        }
        private void CalculateDelta()
        {
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
        }
        static int GCD(int x, int y)
        {
            return y == 0 ? x : GCD(y, x % y);
        }
        private void ReversePass()        
        {
            if (determinant != 0)
            {
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
            }
            //---------------------------------------------
            // if determinant = 0 and SLAE has an infinity number of solutions
            else
            {
                SolveMultipleSolutions();
            }
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
            {
                E[i] = intact[i, freeMember];
                for (int j = 0; j < X.Length; j++)
                {
                    E[i] -= (X[j] * intact[i, j]);
                }
            }
        }
        private Dictionary<int, int[]> CheckBasis()
        {
            
            Dictionary<int, int[]> basisDict = new Dictionary<int, int[]>();
            int similarRowNumber = -1;
            for (int i = 0; i < numberOfbasisVars; i++)
            {
                for (int row = 0; row < colLength; row++)
                {
                    double[] currentRow = new double[5];
                    double[] similarRow = new double[5];
                    if (row != similarRowNumber)
                    {
                        for (int c = 0; c < rowLength; c++)
                        {
                            currentRow[c] = matrix[row, c];
                        }
                        for (int j = 0; j < rowLength - 1; j++)
                        {
                            if (matrix[row, j] != 0)
                            {   
                                if (!basisDict.Keys.Contains(j)) basisDict.Add(j, new int[] { row, j });
                                break;
                            }
                        }
                    };
                    similarRowNumber = CheckSimilarRow(currentRow, row, out similarRow);
                }
            }
            return basisDict;
        }
        private void SolveMultipleSolutions ()
        {
            Dictionary<int, int[]> basisCoords = CheckBasis();

            for (int col = colLength; col > 0; col--)
            {
                foreach (var x in basisCoords)
                {
                    X[x.Key] = matrix[x.Value[0], freeMember];
                    for (int i = 1; i < colLength - x.Value[1]; i++)
                    {
                        int xToMinus = x.Value[1] + i;
                        if (basisCoords.Keys.Contains(xToMinus))
                        {
                            X[x.Key] -= X[xToMinus] * matrix[x.Value[0], xToMinus];
                        } else X[xToMinus] = 0;
                    }
                    X[x.Key] /= matrix[x.Value[0], x.Value[1]];
                }
            }

        }
        private int CheckSimilarRow(double[] currentRow, int row, out double[] similarRow)
        {
            double[] temp;
            int similarRowNumber = 0;
            for (int i = 0; i < colLength; i++)
            {
                if (i != row)
                {
                    temp = new double[currentRow.Length];
                    for (int j = 0; j < rowLength; j++)
                    {
                        if (currentRow[j] != matrix[i, j])
                        {
                            temp = null;
                            break;
                        }
                        temp[j] = currentRow[j];
                    }
                    if (temp != null)
                    {
                        similarRow = temp;
                        similarRowNumber = i;
                        return similarRowNumber;
                    };
                }
            }
            similarRow = null;
            return similarRowNumber;
        } 
    }
}
