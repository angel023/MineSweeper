using System;
using System.Collections.Generic;
// Removed 'gameBoard' after System.Linq
using System.Linq;
using System.Text;

// Initial commit!

namespace MinesweeperProject
{
    //rename class form ttelrik to current
    class MinesweeperMain
    {
        // rename everywhere  atrica to arrayofMines
        private static int[,] arrayOfMines = new int[5, 10];
        // rename everywhere  nekviChisla to randomNumber.......I maked numbers becouse is not only one number
        private static int[] randomNumbers = new int[15];
        //rename everywhere state to gameBoard......I think gameField is little better
        private static int[,] gameField = new int[5, 10];

        //rename <open> to <isCellOpen>
        //I rename it to OpenCells becouse this array hold the information for the open cells, and isCellOpen is more 
        //appropriate for boolean variable
        private static int[,] OpenCells = new int[5, 10];

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
                if (randomNumbers[i] == number)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        //This method initialise all game field not only the mines array
        private static void InitializeNewGameField()
        {
            ClearTheField();

            InitialiseTheMinesOnTheField();
        }

        //I extracted this like a new method
        private static void ClearTheField()
        {
            for (int row = 0; row < gameField.GetLength(0); row++) //change i to row and make magic number 5 to gameField.GetLength(0);
            {
                for (int col = 0; col < gameField.GetLength(1); col++)// change j to col and make magic number 10 to gameField.GetLength(1);
                {
                    arrayOfMines[row, col] = 0;
                    gameField[row, col] = 0;
                    OpenCells[row, col] = 0;
                }
            }
        }

        //I extracted this like a new method and renamed everything inside
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

        //I changed the name of DisplayArrayOfMines with DisplayGameField because this method load the whole field.
        //And make some refactoring
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
        //                            Console.Write("{0} ", CountSurroundingMines(i, j - 2));
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

        //removed nonsence comments
        //I fixed big bug here now displayTop works good
        //I renamed to displayScoreBoard becouse this is what actually this method do
        private static void DisplayScoreBoard()
        {
            Console.WriteLine("Scoreboard: {0}", Environment.NewLine);
            //for (int i = 0; i < playerScoreCounter % 6; i++)
            for (int i = 0; i < playerScore.Length; i++)
            {
                Console.WriteLine("{0}. {1} --> {2} cells", i, playerName[i], playerScore[i]);
            }
        }

        //refactor the method in quallity terms origirn is return ( 0<=i && i<=4) && (0<=j && j<=9)
        //I also make little canges here I hope you liked better this way? And also change the name becouse this method
        //check is the position valid not the dimensions
        //Changed CheckIsThePosiotionValid to IsPositionValid
        private static bool IsPositionValid(int row, int col)
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

        // rename res to result 
        //Rename the method to CountOpenCells an rename some variables
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

        static void Main(string[] argumenti)
        {
            //InitializeNewGameField();
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

            //Console.WriteLine("{0}", CountSurroundingMines(3,0));
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
            InitializeNewGameField();
            DisplayGameField();

            while (playerMove != "exit")//It must be exit not end
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
                        DisplayScoreBoard();
                    }
                    Console.WriteLine("Welcome to the game “Minesweeper”.{0} Try to reveal all cells without mines."+
                        " Use 'TOP' to view the scoreboard, {0}'RESTART' to start a new game and 'EXIT' to quit the game.",
                        Environment.NewLine);

                    //Initialise the field with the start of the game
                    Console.WriteLine();
                    InitializeNewGameField();
                    DisplayGameField();
                }
                //Instead of f:. I also added a check to see if the command is correct
                //I add a check for the space between the row and col input, becouse I think it is not correct input 123 for example
                //and this will be correct without this check, but I still dont like it.Maybe we must think for some method for 
                //parsing the input.
                else if (playerMove.Length == 3 && int.TryParse(playerMove[0].ToString(), out moveToRow)
                    && int.TryParse(playerMove[2].ToString(), out moveToColumn) && playerMove[1].ToString() == " ")
                {                                                                                                   
                    Console.WriteLine(moveToRow);

                    if (OpenCells[moveToRow, moveToColumn] == 0)
                    {
                        OpenCells[moveToRow, moveToColumn] = 1;
                        gameField[moveToRow, moveToColumn] = 1;
                        playerScoreCounter += 1;
                        if (arrayOfMines[moveToRow, moveToColumn] == 1)
                        {
                            for (int boardRow = 0; boardRow < gameField.GetLength(0); boardRow++) //I changed i with boardRow and the lenght of the
                            {                                                                     //loop with gameField.GetLength(0) instead magic number 5   
                                for (int boardCol = 0; boardCol < gameField.GetLength(1); boardCol++) //I changed j with boardCol and the lenght of the
                                {                                                                     //loop with gameField.GetLength(1) instead magic number 10   
                                    gameField[boardRow, boardCol] = 1;
                                }
                            }

                            DisplayGameField();
                            // rewrite some of the message 
                            Console.WriteLine("Booooom! You were killed by a mine. {0} You score is {1}. Please enter your name for the top scoreboard: ", Environment.NewLine, playerScoreCounter-1);

                            string name = Console.ReadLine();

                            //Very big bug fixed! This way we can make gameboard, becouse in the original way it was no sorted.
                            Array.Sort(playerScore);
                            for (int i = 0; i < playerScore.Length; i++)
                            {
                                if (playerScoreCounter>playerScore[i])
                                {
                                    playerScore[i] = playerScoreCounter-1;
                                    playerName[i] = name;
                                    break;
                                }
                            }
                            playerScoreCounter = 0;
                            //playerName[playerScoreCounter % 5] = name;
                            //playerScore[playerScoreCounter % 5] = CountOpenCells() - 1;
                            
                            //This was a big bug!!!After game over starts a new game on the old field and the old field is already
                            //open so every move is market like not valid
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
                // Change from "(playersMove.Length < 3)". Now it checks for all invalid commands.
                else if (playerMove != "exit")
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