using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinesweeperProject
{
    class MinesweeperMain
    {
        private static int[,] arrayOfMines = new int[5, 10];
        private static int[] randomNumbers = new int[15];
        private static int[,] gameField = new int[5, 10];
        private static int[,] OpenCells = new int[5, 10];
        private static int[] playersScores = new int[5];
        private static string[] playersNames = new string[5];
        private static int playerScoreCounter = 0;
        private static bool IsFoundInRandomNumbers(int index, int number)
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

        private static void InitializeNewGameField()
        {
            ClearTheField();

            InitialiseTheMinesOnTheField();
        }

        private static void ClearTheField()
        {
            for (int row = 0; row < gameField.GetLength(0); row++) 
            {
                for (int col = 0; col < gameField.GetLength(1); col++)
                {
                    arrayOfMines[row, col] = 0;
                    gameField[row, col] = 0;
                    OpenCells[row, col] = 0;
                }
            }
        }

        private static void InitialiseTheMinesOnTheField()
        {
            Random randomGenerator = new Random();

            for (int index = 0; index < randomNumbers.Length; index++)
            {
                int randomNumber = randomGenerator.Next(50);

                while (IsFoundInRandomNumbers(index, randomNumber))
                {
                    randomNumber = randomGenerator.Next(50);
                }

                randomNumbers[index] = randomNumber;
                arrayOfMines[(randomNumber / 10), (randomNumber % 10)] = 1;
            }
        }

        private static void DisplayGameField()
        {
            DisplayColNumbers();

            DisplayHorizontalWall();

            for (int row = 0; row < gameField.GetLength(0); row++)
            {
                Console.Write(row + " | ");

                DisplaySingleFieldRow(row);

                Console.WriteLine("|");
            }

            DisplayHorizontalWall();
        }

        private static void DisplaySingleFieldRow(int row)
        {
            for (int col = 0; col < gameField.GetLength(1); col++)
            {
                if (gameField[row, col] == 0)
                {
                    Console.Write("? ");
                }
                else
                {
                    if (arrayOfMines[row, col] == 1)
                    {
                        Console.Write("* ");
                    }
                    else
                    {
                        if (OpenCells[row, col] == 1)
                        {
                            Console.Write("{0} ", CountSurroundingMines(row, col));
                        }
                        else
                        {
                            Console.Write("- ");
                        }
                    }
                }
            }
        }

        private static void DisplayColNumbers()
        {
            Console.Write("    ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine("");
        }

        private static void DisplayHorizontalWall()
        {
            Console.Write("   ");
            for (int i = 0; i < 21; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }

        private static int CountSurroundingMines(int currentRow, int currentCol)
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
                        arrayOfMines[currentRow + row, currentCol + col] == 1)
                    {
                        minesCounter++;
                    }
                }
            }

            return minesCounter;
        }

        private static void DisplayScoreBoard()
        {
            Console.WriteLine("Scoreboard: {0}", Environment.NewLine);
            for (int i = 0; i < playersScores.Length; i++)
            {
                Console.WriteLine("{0}. {1} --> {2} cells", i, playersNames[i], playersScores[i]);
            }
        }
       
        private static bool IsPositionValid(int row, int col)
        {
            bool isRowValid = false; 
            if (0 <= row && row <= 4)
            {
                isRowValid = true;
            }

            bool isColumnValid = false;
            if (0 <= col && col <= 9)
            {
                isColumnValid = true; 
            }

            bool isValidPosition = (isRowValid && isColumnValid); 

            return isValidPosition;
        }

        private static int CountOpenCells()
        {
            int result = 0;
            for (int row = 0; row < gameField.GetLength(0); row++)
            {
                for (int col = 0; col < gameField.GetLength(1); col++)
                {
                    if (OpenCells[row, col] == 1)
                    {
                        result++;
                    }
                }
            }
            return result;
        }

        private static void MakeAMove(int moveToRow, int moveToColumn)
        {
            OpenCells[moveToRow, moveToColumn] = 1;
            gameField[moveToRow, moveToColumn] = 1;
            playerScoreCounter += 1;
        }

        private static void EnterPlayerResult()
        {
            string name = Console.ReadLine();

            Array.Sort(playersScores);
            for (int i = 0; i < playersScores.Length; i++)
            {
                if (playerScoreCounter > playersScores[i])
                {
                    playersScores[i] = playerScoreCounter - 1;
                    playersNames[i] = name;
                    break;
                }
            }
            playerScoreCounter = 0;
        }

        private static void RevealGameField()
        {
            for (int boardRow = 0; boardRow < gameField.GetLength(0); boardRow++) 
            {                                                                     
                for (int boardCol = 0; boardCol < gameField.GetLength(1); boardCol++) 
                {                                                                       
                    gameField[boardRow, boardCol] = 1;
                }
            }
        }

        private static void StartNewGame()
        {
            Console.WriteLine("Welcome to the game “Minesweeper”.{0} Try to reveal all cells without mines. Use 'TOP' to view the scoreboard, {0}'RESTART' to start a new game and 'EXIT' to quit the game.", Environment.NewLine);
            Console.WriteLine();
            InitializeNewGameField();
            DisplayGameField();
        }

        static void Main()
        {

            StartNewGame();

            string playerMove = "";
            while (playerMove != "exit")
            {
                Console.WriteLine("{0} Please input your move: ", Environment.NewLine);
                playerMove = Console.ReadLine();

                int moveToRow;
                int moveToColumn;

                if (playerMove == "top" || playerMove == "restart")
                {
                    if (playerMove == "top")
                    {
                        DisplayScoreBoard();
                    }

                    StartNewGame();
                }
                else if (playerMove.Length == 3 && int.TryParse(playerMove[0].ToString(), out moveToRow)
                    && int.TryParse(playerMove[2].ToString(), out moveToColumn) && playerMove[1].ToString() == " ")
                {                                                                                                   
                    Console.WriteLine(moveToRow);

                    if (OpenCells[moveToRow, moveToColumn] == 0)
                    {
                        MakeAMove(moveToRow, moveToColumn);

                        if (arrayOfMines[moveToRow, moveToColumn] == 1)
                        {  
                            RevealGameField();
                            DisplayGameField();
                            Console.WriteLine("Booooom! You were killed by a mine. {0} You score is {1}. Please enter your name for the top scoreboard: ", Environment.NewLine, playerScoreCounter-1);
                            EnterPlayerResult();
                            InitializeNewGameField();
                            DisplayGameField();
                        }
                        else
                        {
                            Console.WriteLine(CountSurroundingMines(moveToRow, moveToColumn));
                            DisplayGameField();
                        }
                    }

                    else if (OpenCells[moveToRow, moveToColumn] == 1)
                    {
                        Console.WriteLine("Illegal move!");
                    }
                }
                else if (playerMove != "exit")
                {
                    Console.WriteLine("Illegal input");
                }
            }
            Console.WriteLine();

            Console.WriteLine("Good Bye!!!");
        }
    }
}