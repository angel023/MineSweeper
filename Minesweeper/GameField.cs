using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minesweeper
{
    class GameField
    {
        public int[,] ArrayOfMines { get; private set; }
        public int[,] OpenCells { get; private set; }
        public int[,] Field { get; private set; } //Changed GameField to Field

        //Instead of initializing the three arrays separately with the same values.
        public GameField(int rowCount, int columnCount)
        {
            this.ArrayOfMines = new int[rowCount, columnCount];
            this.OpenCells = new int[rowCount, columnCount];
            this.Field = new int[rowCount, columnCount];
        }
        public void InitializeNewGameField()
        {
            ClearTheField();
            InitialiseTheMinesOnTheField();
        }

        // All the Display methods are changed to Get and return a string instead of writing directly on the console.
        public override string ToString()
        {
            StringBuilder gameFieldBuilder = new StringBuilder();
            gameFieldBuilder.Append(GetColNumbersString());

            gameFieldBuilder.Append(GetHorizontalWallString());

            for (int row = 0; row < this.Field.GetLength(0); row++)
            {
                gameFieldBuilder.Append(row + " | ");

                gameFieldBuilder.Append(GetSingleFieldRowString(row));

                gameFieldBuilder.AppendLine("|");
            }

            gameFieldBuilder.Append(GetHorizontalWallString());

            return gameFieldBuilder.ToString();
        }

        public int CountSurroundingMines(int currentRow, int currentCol)
        {
            int minesCounter = 0;

            for (int row = -1; row < 2; row++)
            {
                for (int col = -1; col < 2; col++)
                {
                    if (col == 0 && row == 0)
                    {
                        continue;
                    }
                    if (IsPositionValid(currentRow + row, currentCol + col) &&
                        this.ArrayOfMines[currentRow + row, currentCol + col] == 1)
                    {
                        minesCounter++;
                    }
                }
            }

            return minesCounter;
        }

        private bool IsPositionValid(int row, int col)
        {
            bool isRowValid = false; //Its more appropriate the name of bool to start with "Is"
            if (0 <= row && row <= 4)
            {
                isRowValid = true;
            }

            bool isColumnValid = false;
            if (0 <= col && col <= 9)
            {
                isColumnValid = true; //Its more appropriate the name of bool to start with "Is"
            }

            bool isValidPosition = (isRowValid && isColumnValid); //This is validation for the position not for the dimensions

            return isValidPosition;
        }

        private int CountOpenCells()
        {
            int result = 0;
            for (int row = 0; row < this.Field.GetLength(0); row++)
            {
                for (int col = 0; col < this.Field.GetLength(1); col++)
                {
                    if (this.OpenCells[row, col] == 1)
                    {
                        result++;
                    }
                }
            }
            return result;
        }
        
        private string GetSingleFieldRowString(int row)
        {
            StringBuilder fieldRowBuilder = new StringBuilder();
            for (int col = 0; col < this.Field.GetLength(1); col++)
            {
                if (this.Field[row, col] == 0)
                {
                    fieldRowBuilder.Append("? ");
                }
                else
                {
                    if (this.ArrayOfMines[row, col] == 1)
                    {
                        fieldRowBuilder.Append("* ");
                    }
                    else
                    {
                        if (OpenCells[row, col] == 1)
                        {
                            fieldRowBuilder.Append(String.Format("{0} ", CountSurroundingMines(row, col)));
                        }
                        else
                        {
                            fieldRowBuilder.Append("- ");
                        }
                    }
                }
            }
            return fieldRowBuilder.ToString();
        }

        private string GetColNumbersString()
        {
            StringBuilder ColNumbersBuilder = new StringBuilder();
            ColNumbersBuilder.Append("    ");

            for (int i = 0; i < 10; i++)
            {
                ColNumbersBuilder.Append(String.Format("{0} ", i));
            }

            ColNumbersBuilder.AppendLine();
            return ColNumbersBuilder.ToString();
        }

        private string GetHorizontalWallString()
        {
            StringBuilder horizontalWallBuilder = new StringBuilder();
            horizontalWallBuilder.Append("   ");
            for (int i = 0; i < 21; i++)
            {
                horizontalWallBuilder.Append("-");
            }
            horizontalWallBuilder.AppendLine();

            return horizontalWallBuilder.ToString();
        }

        private void ClearTheField()
        {
            for (int row = 0; row < this.Field.GetLength(0); row++) //change i to row and make magic number 5 to gameField.GetLength(0);
            {
                for (int col = 0; col < this.Field.GetLength(1); col++)// change j to col and make magic number 10 to gameField.GetLength(1);
                {
                    this.ArrayOfMines[row, col] = 0;
                    this.Field[row, col] = 0;
                    this.OpenCells[row, col] = 0;
                }
            }
        }

        //I extracted this like a new method and renamed everything inside
        private void InitialiseTheMinesOnTheField()
        {
            Random randomGenerator = new Random();

            int[] randomNumbers = new int[15];
            for (int index = 0; index < randomNumbers.Length; index++)
            {
                int randomNumber = randomGenerator.Next(50);

                while (IsFoundInRandomNumbers(index, randomNumber, randomNumbers))
                {
                    randomNumber = randomGenerator.Next(50);
                }

                randomNumbers[index] = randomNumber;
                this.ArrayOfMines[(randomNumber / 10), (randomNumber % 10)] = 1;
            }
        }

        private bool IsFoundInRandomNumbers(int index, int number, int[] randomNumbers)
        {
            bool result = false;
            for (int i = 0; i < index - 1; i++)
            {
                if (randomNumbers[i] == number)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
