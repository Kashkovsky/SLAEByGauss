using NUnit.Framework;
using SLAEByGauss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLAEByGauss.Tests
{
    [TestFixture()]
    public class SolverTests
    {
        private double[,] _matrix = new double[100, 101];
        private Solver _solver;

        [Test()]
        public void SolverTest()
        {
            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 101; j++)
                {
                    _matrix[i, j] = random.Next(100);
                }   
            }
            _solver = new Solver(_matrix);
        }

        [Test()]
        public void CheckConsistencyTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void SimpleConversionTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void SwapRowsTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void CalculateDeltaTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GcdTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void ReversePassTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void CheckBasisTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void SolveMultipleSolutionsTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void CheckSimilarRowTest()
        {
            Assert.Fail();
        }
    }
}