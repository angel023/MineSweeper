using System;
using System.Collections.Generic;
// Removed 'gameBoard' after System.Linq
using System.Linq;
using System.Text;

// Initial commit!

namespace Minesweeper
{
    //rename class form ttelrik to current
    class MinesweeperProject
    {
        // rename everywhere  atrica to arrayofMines
        private static int[,] arrayOfMines = new int[5, 10];
        // rename everywhere  nekviChisla to randomNumber
        private static int[] randomNumber = new int[15];
        //rename everywhere state to gameBoard
        private static int[,] gameBoard = new int[5, 10];

        //rename <open> to <isCellOpen>
        private static int[,] isCellOpen = new int[5, 10];

        // rename topCells to playerScore
        private static int[] playerScore = new int[5];

        // rename everywhere topNames to playerName
        private static string[] playerName = new string[5];
        // rename topCellsCounter to playerScoreCounter
        private static int playerScoreCounter = 0;

        //delete some empty lines
        private static bool IsFoundInRandomNumbers(int index, int number)
        {
            bool result = false;
            for (int i = 0; i < index - 1; i++)
            {
                if (randomNumber[i] == number)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }


        private static void InitializeArrayOfMines()
        {
            for (int row = 0; row < 5; row++) //change i to row
            {
                for (int col = 0; col < 10; col++)// change j to col
                {
                    arrayOfMines[row, col] = 0;
                    gameBoard[row, col] = 0;
                    isCellOpen[row, col] = 0;
                }
            }

            Random random = new Random();

            for (int i = 0; i < 15; i++)
            {
                int index = random.Next(50);

                while (IsFoundInRandomNumbers(i, index))
                {
                    index = random.Next(50);
                }

                randomNumber[i] = index;
                arrayOfMines[(index / 10), (index % 10)] = 1;
            }
        }

        //I changed the name of DisplayArrayOfMines with DisplayGameField because this method load the whole field.
        //And make some refactoring
        private static void DisplayGameField()
        {
            DisplayColNumbers();

            DisplayHorizontalWall();

            for (int row = 0; row < 5; row++)
            {
                Console.Write(row + " | ");

                DisplaySingleFieldRow(row);

                Console.WriteLine("|");
            }

            DisplayHorizontalWall();
        }

        private static void DisplaySingleFieldRow(int row)
        {
            for (int col = 0; col < 10; col++)
            {
                if (gameBoard[row, col] == 0)
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
                        if (isCellOpen[row, col] == 1)
                        {
                            Console.Write("{0} ", CountNeighborcell(row, col));
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

        //private static void DisplayGameField()
        //{
        //    Console.Write("    ");

        //    for (int i = 0; i < 10; i++)
        //    {
        //        Console.Write("{0} ", i);
        //    }

        //    Console.WriteLine("");
        //    Console.Write("    ");

        //    for (int i = 0; i < 21; i++)
        //    {
        //        Console.Write("-");
        //    }

        //    Console.WriteLine("");

        //    for (int i = 0; i < 5; i++)
        //    {
        //        for (int j = 0; j < 13; j++)
        //        {
        //            if (2 <= j && j <= 11)
        //            {
        //                if (gameBoard[i, j - 2] == 0)
        //                {
        //                    Console.Write("? ");
        //                }
        //                else
        //                {
        //                    if (arrayOfMines[i, j - 2] == 1)
        //                    {
        //                        Console.Write("* ");
        //                    }
        //                    else
        //                    {
        //                        if (isCellOpen[i, j - 2] == 1)
        //                        {
        //                            Console.Write("{0} ", CountNeighborcell(i, j - 2));
        //                        }
        //                        else
        //                        {
        //                            Console.Write("- ");
        //                        }
        //                    }
        //                }
        //            }
        //            if (j == 1 || j == 12)
        //            {
        //                Console.Write("| ");
        //            }
        //            if (j == 0)
        //            {
        //                Console.Write("{0} ", i);
        //            }
        //        }
        //        Console.WriteLine("");
        //    }

        //    Console.Write("    ");

        //    for (int i = 0; i < 21; i++)
        //    {
        //        Console.Write("-");
        //    }

        //    Console.WriteLine("");
        //}

        private static int CountNeighborcell(int i, int j)
        {
            int counter = 0;

            for (int i1 = -1; i1 < 2; i1++)
            {

                for (int j1 = -1; j1 < 2; j1++)
                {
                    if (j1 == 0 && i1 == 0)
                        continue;
                    if (CheckBoardDimensions(i + i1, j + j1) && arrayOfMines[i + i1, j + j1] == 1)
                    {
                        counter++;
                    }
                }
            }

            return counter;
        }

        //removed nonsence comments
        private static void DisplayTop()
        {
            Console.WriteLine("Scoreboard: {0}", Environment.NewLine);
            for (int i = 0; i < playerScoreCounter % 6; i++)
            {
                Console.WriteLine("{0}. {1} --> {2} cells", i, playerName[i], playerScore[i]);
            }
        }

        //refactor the method in quallity terms origirn is return ( 0<=i && i<=4) && (0<=j && j<=9)
        private static bool CheckBoardDimensions(int i, int j)
        {
            bool validationRow;
            bool validationColumn;
            if (0 <= i && i <= 4)
            {
                validationRow = true;
            }
            else
            {
                validationRow = false;
            }
            if (0 <= j && j <= 9)
            {
                validationColumn = true;
            }
            else
            {
                validationColumn = false;
            }
            bool validBoardDimensions;
            if (validationRow == true && validationColumn == true)
            {
                validBoardDimensions = true;
            }
            else
            {
                validBoardDimensions = false;
            }
            return validBoardDimensions;
        }

        // rename res to result 
        private static int CountOpen()
        {
            int result = 0;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 10; j++)
                {
                    if (isCellOpen[i, j] == 1)
                    {
                        result++;
                    }
                }
            return result;
        }

        static void Main(string[] argumenti)
        {
            //InitializeArrayOfMines();
            /* for (int i = 0; i < 15; i++)
             {
                 Console.WriteLine(1);
                 Console.WriteLine(randomNumbers[i]);
             }*/
            //for (int i = 0; i < 5; i++)
            //{
            //    for (int j = 0; j < 10; j++)
            //    {
            //        Console.Write(arrayOfMines[i, j]);
            //    }
            //    Console.WriteLine();
            //}

            //Console.WriteLine("{0}", CountNeighborcell(3,0));
            //gameBoard[1, 3] = 1;
            //gameBoard[2, 4] = 1;
            //gameBoard[0, 0] = 1;
            //gameBoard[2, 1] = 1;
            //gameBoard[1, 0] = 1;

            //DisplayGameField();

            //TO DO: Some REFACTORING Used for go to !!!

            // Changed playersMove to playerMove. The string is initialized before the loop starts. TO DO: maybe refactor to avoid repeating code.
            string playerMove = "";
            Console.WriteLine("Welcome to the game “Minesweeper”.{0} Try to reveal all cells without mines. Use 'TOP' to view the scoreboard, {0}'RESTART' to start a new game and 'EXIT' to quit the game.", Environment.NewLine);

            //Initialise the field in the beginning of the game
            Console.WriteLine();
            InitializeArrayOfMines();
            DisplayGameField();

            while (playerMove != "end")
            {
                Console.WriteLine("{0} Please input your move: ", Environment.NewLine);
                playerMove = Console.ReadLine();
                // rename p1 to moveToRow
                int moveToRow;
                int moveToColumn;

                // Instead of begin:
                if (playerMove == "top" || playerMove == "restart")
                {
                    if (playerMove == "top")
                    {
                        DisplayTop();
                    }
                    Console.WriteLine("Welcome to the game “Minesweeper”.{0} Try to reveal all cells without mines. Use 'TOP' to view the scoreboard, {0}'RESTART' to start a new game and 'EXIT' to quit the game.", Environment.NewLine);

                    //Initialise the field with the start of the game
                    Console.WriteLine();
                    InitializeArrayOfMines();
                    DisplayGameField();
                }
                //Instead of f:. I also added a check to see if the command is correct
                else if (playerMove.Length == 3 && int.TryParse(playerMove[0].ToString(), out moveToRow)
                    && int.TryParse(playerMove[2].ToString(), out moveToColumn))
                {
                    Console.WriteLine(moveToRow);

                    if (isCellOpen[moveToRow, moveToColumn] == 0)
                    {
                        isCellOpen[moveToRow, moveToColumn] = 1;
                        gameBoard[moveToRow, moveToColumn] = 1;

                        if (arrayOfMines[moveToRow, moveToColumn] == 1)
                        {
                            for (int boardRow = 0; boardRow < gameBoard.GetLength(0); boardRow++) //I changed i with boardRow and the lenght of the
                            {                                                                     //loop with gameBoard.GetLength(0) instead magic number 5   
                                for (int boardCol = 0; boardCol < gameBoard.GetLength(1); boardCol++) //I changed j with boardCol and the lenght of the
                                {                                                                     //loop with gameBoard.GetLength(1) instead magic number 10   
                                    gameBoard[boardRow, boardCol] = 1;
                                }
                            }

                            DisplayGameField();
                            // rewrite some of the message 
                            Console.WriteLine("Booooom! You were killed by a mine. {0} You score is {1}. Please enter your name for the top scoreboard: ", Environment.NewLine, playerScoreCounter);

                            string name = Console.ReadLine();

                            playerName[playerScoreCounter % 5] = name;
                            playerScore[playerScoreCounter % 5] = CountOpen() - 1;
                        }
                        else
                        {
                            Console.WriteLine(CountNeighborcell(moveToRow, moveToColumn));
                            DisplayGameField();
                        }
                    }

                    else if (isCellOpen[moveToRow, moveToColumn] == 1)
                    {
                        Console.WriteLine("Illegal move!");
                    }
                }
                // Change from "(playersMove.Length < 3)". Now it checks for all invalid commands.
                else if (playerMove != "end")
                {
                    Console.WriteLine("Illegal input");
                }
            }
            // remove //Console.WriteLine(w==q);
            Console.WriteLine();

            Console.WriteLine("Good Bye!!!");
        }
    }
}