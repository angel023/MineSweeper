using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinesweeperProject;
using System.Text;

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
            var result = gameField.IsCellOpen(0, 2);

            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void IsCellOpenOpenCellTest()
        {
            GameField gameField = new GameField(5, 10);

            gameField.OpenCells[0, 2] = 1;
            var result = gameField.IsCellOpen(0, 1);

            Assert.AreEqual(result, true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IsCellOpenWithNegativeValueTest()
        {
            GameField gameField = new GameField(5, 10);

            gameField.OpenCells[0, 2] = 1;
            var result = gameField.IsCellOpen(0, -1);

            Assert.AreEqual(result, true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IsCellOpenWithOutOfBoundsValueTest()
        {
            GameField gameField = new GameField(5, 10);

            gameField.OpenCells[0, 2] = 1;
            var result = gameField.IsCellOpen(0, 10);
            
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void ClearTheFieldTest()
        {
            GameField gameField = new GameField(5, 10);
            gameField.Field[0, 1] = 1;

            PrivateObject obj = new PrivateObject(gameField);
            obj.Invoke("ClearTheField");

            Assert.AreEqual(gameField.Field[0, 1], 0);
        }

        [TestMethod]
        public void RevealGameFieldTest()
        {
            GameField gameField = new GameField(5, 10);
            gameField.RevealGameField();

            for (int row = 0; row < gameField.Field.GetLength(0); row++)
            {
                for (int col = 0; col < gameField.Field.GetLength(1); col++)
                {
                    Assert.AreEqual(gameField.Field[row, col], 1);
                }
            }
        }

        [TestMethod]
        public void IsPositionValidForValidPositionTest()
        {
            GameField gameField = new GameField(5, 10);

            PrivateObject obj = new PrivateObject(gameField);
            var valid = obj.Invoke("IsPositionValid", 1, 2);

            Assert.AreEqual(valid, true);
        }

        [TestMethod]
        public void IsPositionValidForInValidPositionTest()
        {
            GameField gameField = new GameField(5, 10);

            PrivateObject obj = new PrivateObject(gameField);
            var valid = obj.Invoke("IsPositionValid", 10, 2);

            Assert.AreEqual(valid, false);
        }


        [TestMethod]
        public void CountOpenCellsTest()
        {
            GameField gameField = new GameField(5, 10);
            gameField.OpenCells[0, 1] = 1;
            gameField.OpenCells[3, 3] = 1;
            gameField.OpenCells[2, 1] = 1;

            PrivateObject obj = new PrivateObject(gameField);
            var numberOfOpenCells = obj.Invoke("CountOpenCells");

            Assert.AreEqual(numberOfOpenCells, 3);
        }

        [TestMethod]
        public void GetSingleFieldRowStringTest()
        {
            GameField gameField = new GameField(5, 10);

            PrivateObject obj = new PrivateObject(gameField);
            var row = obj.Invoke("GetSingleFieldRowString", 1);
            string expected = "? ? ? ? ? ? ? ? ? ? ";

            Assert.AreEqual(row, expected);
        }

        [TestMethod]
        public void GetSingleFieldRowStringWithMinesAndVisitedCellTest()
        {
            GameField gameField = new GameField(5, 10);
            gameField.ArrayOfMines[1, 2] = 1;
            gameField.ArrayOfMines[1, 4] = 1;
            gameField.OpenCells[1, 3] = 1;
            gameField.Field[1, 3] = 1;
            PrivateObject obj = new PrivateObject(gameField);
            var row = obj.Invoke("GetSingleFieldRowString", 1);
            string expected = "? ? ? 2 ? ? ? ? ? ? ";

            Assert.AreEqual(row, expected);
        }

        [TestMethod]
        public void GetColNumbersStringTest()
        {
            GameField gameField = new GameField(5, 10);
            StringBuilder sb = new StringBuilder();
            PrivateObject obj = new PrivateObject(gameField);
            sb.Append("    0 1 2 3 4 5 6 7 8 9 ");
            sb.AppendLine();
            var result = obj.Invoke("GetColNumbersString");
            string expected = sb.ToString();

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void GetHorizontalWallStringTest()
        {
            GameField gameField = new GameField(5, 10);
            StringBuilder sb = new StringBuilder();
            PrivateObject obj = new PrivateObject(gameField);
            sb.Append("   ---------------------");
            sb.AppendLine();
            var result = obj.Invoke("GetHorizontalWallString");
            string expected = sb.ToString();

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void IsFoundInRandomNumbersTest()
        {
            GameField gameField = new GameField(5, 10);
            StringBuilder sb = new StringBuilder();
            PrivateObject obj = new PrivateObject(gameField);
            int[] array = new int[] { 1, 5, 3 };
            var result = obj.Invoke("IsFoundInRandomNumbers", 3, 5, array);
            bool expected = true;

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void ToStringTest()
        {
            GameField gameField = new GameField(2, 10);
            gameField.ArrayOfMines[0, 2] = 1;
            gameField.ArrayOfMines[0, 4] = 1;
            gameField.OpenCells[0, 3] = 1;
            gameField.Field[0, 3] = 1;
            StringBuilder sb = new StringBuilder();
            sb.Append("    0 1 2 3 4 5 6 7 8 9 ");
            sb.AppendLine();
            sb.Append("   ---------------------");
            sb.AppendLine();
            sb.Append("0 | ");
            sb.Append("? ? ? 2 ? ? ? ? ? ? ");
            sb.AppendLine("|");
            sb.Append("1 | ");
            sb.Append("? ? ? ? ? ? ? ? ? ? ");
            sb.AppendLine("|");
            sb.Append("   ---------------------");
            sb.AppendLine();

            var result = gameField.ToString();

            string expected = sb.ToString();

            Assert.AreEqual(result, expected);
        }
    }
}
