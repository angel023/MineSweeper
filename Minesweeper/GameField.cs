using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinesweeperProject
{
    public class GameField
    {
        /// <summary>
        /// Gets the created Array of Mines in Game Field
        /// </summary>
        public int[,] ArrayOfMines { get; private set; }

        /// <summary>
        /// Gets the currently Open Cells in the Game field
        /// </summary>
        public int[,] OpenCells { get; private set; }

        /// <summary>
        /// Gets the game field two dimensional
        /// </summary>
        public int[,] Field { get; private set; } //Changed GameField to Field

        /// <summary>
        /// Initializes a new instance of the class GameField
        /// Constructor of the GameField class 
        /// </summary>
        /// <param name="rowCount">Number of Rows</param>
        /// <param name="columnCount">Number of Columns</param>
        /// <remarks>
        /// Both parameters must be positive integer in order to avoid exceptions.
        /// </remarks>
        public GameField(int rowCount, int columnCount) //Instead of initializing the three arrays separately with the same values.
        {
            if (rowCount<=0 || columnCount<=0)
            {
                throw new ArgumentOutOfRangeException("The field rows and columns must be positive numbers");
            }

            this.ArrayOfMines = new int[rowCount, columnCount];
            this.OpenCells = new int[rowCount, columnCount];
            this.Field = new int[rowCount, columnCount];
        }

        /// <summary>
        /// Create New GameField if previous one exist.
        /// </summary>
        public void InitializeNewGameField()
        {
            ClearTheField();
            InitialiseTheMinesOnTheField();
        }

        /// <summary>
        /// Method that check the cell of the game field is it open or not.
        /// </summary>
        /// <param name="row">Current move row</param>
        /// <param name="column">Current move column</param>
        /// <returns>true if is open and false if is not revealed yet.</returns>
        public bool IsCellOpen(int row, int column) 
        {
            if (row<0 || column<0)
            {
                throw new ArgumentOutOfRangeException("Row and column must be positive numbers");
            }

            if (row>= OpenCells.GetLength(0) || column>= OpenCells.GetLength(1))
            {
                throw new ArgumentOutOfRangeException("Row and column must be smaller than tha array bounds");
            }

            if (this.OpenCells[row, column] == 0)
            {
                return true;   
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Override Method To String in order to create Game Field over console.
        /// </summary>
        /// <returns>String of Game field visualize over the console.</returns>
        public override string ToString()
        {
            // All the Display methods are changed to Get and return a string 
            // instead of writing directly on the console.
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

        /// <summary>
        /// Method count number of mines in cell neighbor to the open one.        /// 
        /// </summary>
        /// <param name="currentRow">currently opened row</param>
        /// <param name="currentCol">currently opened column</param>
        /// <returns>Number of mines in neighbor cells</returns>
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

        /// <summary>
        /// Method that reveal whole game field (mine included)
        /// </summary>
        public void RevealGameField()
        {
            for (int boardRow = 0; boardRow < this.Field.GetLength(0); boardRow++)
            {
                for (int boardCol = 0; boardCol < this.Field.GetLength(1); boardCol++)
                {
                    this.Field[boardRow, boardCol] = 1;
                }
            }
        }

        /// <summary>
        /// Method check the input string for valid values (the move must be in game field)
        /// </summary>
        /// <param name="row">input new move to row</param>
        /// <param name="col">input new move to column</param>
        /// <returns>true if position of the player is valid one, false if not</returns>
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

        /// <summary>
        /// Method check number of opened cells
        /// </summary>
        /// <returns>number of opened cells</returns>
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
            StringBuilder colNumbersBuilder = new StringBuilder();
            colNumbersBuilder.Append("    ");

            for (int i = 0; i < 10; i++)
            {
                colNumbersBuilder.Append(String.Format("{0} ", i));
            }

            colNumbersBuilder.AppendLine();
            return colNumbersBuilder.ToString();
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

        private void InitialiseTheMinesOnTheField() //I extracted this like a new method and renamed everything inside
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
