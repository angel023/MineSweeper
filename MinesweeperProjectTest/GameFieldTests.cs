using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinesweeperProject;

namespace MinesweeperProjectTest
{
    [TestClass]
    public class GameFieldTests
    {
        [TestMethod]
        public void GameFieldConstructor()
        {
            GameField gameField = new GameField(5, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GameFieldConstructorWithNegativeNumbers()
        {
            GameField gameField = new GameField(-5, 10);
            
        }

        [TestMethod]
        public void GameFieldPropertiesTest()
        {
            GameField gameField = new GameField(5, 10);
            int[,] matrix = new int[5, 10];

            Assert.AreEqual(matrix.GetLength(0), gameField.Field.GetLength(0));
            Assert.AreEqual(matrix.GetLength(1), gameField.Field.GetLength(1));
        }

        [TestMethod]
        public void IsCellOpenWithNotOpenCellTest()
        {
            GameField gameField = new GameField(5, 10);

            gameField.OpenCells[0, 2] = 1;
            var result = gameField.isCellOpen(0, 2);

            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void IsCellOpenOpenCellTest()
        {
            GameField gameField = new GameField(5, 10);

            gameField.OpenCells[0, 2] = 1;
            var result = gameField.isCellOpen(0, 1);

            Assert.AreEqual(result, true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IsCellOpenWithNegativeValueTest()
        {
            GameField gameField = new GameField(5, 10);

            gameField.OpenCells[0, 2] = 1;
            var result = gameField.isCellOpen(0, -1);

            Assert.AreEqual(result, true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IsCellOpenWithOutOfBoundsValueTest()
        {
            GameField gameField = new GameField(5, 10);

            gameField.OpenCells[0, 2] = 1;
            var result = gameField.isCellOpen(0, 10);

            Assert.AreEqual(result, true);
        }
    }
}
