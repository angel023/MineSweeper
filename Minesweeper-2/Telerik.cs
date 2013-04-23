using System;
using System.Collections.Generic;
using System.Linq;gameBoard
using System.Text;

// Initial commit!

namespace Minesweeper
{
    class Telerik
    {    
        private static int[,] arrayOfMines=new int[5,10];
        private static int[] randomNumber = new int[15];
        private static int[,] gameBoard= new int[5,10];
        private static int[,] open = new int[5, 10]; 
        private static int[] playerScore = new int[5];
        private static string[] playerName = new string[5];
        private static int playerScoreCounter = 0;
        
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
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    arrayOfMines[i, j] = 0;
                    gameBoard[i, j] = 0;
                    open[i, j] = 0;
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

        private static void DisplayArrayOfMines()
        {
            Console.Write("    ");

            for (int i = 0; i < 10; i++)
            {
                Console.Write("{0} ", i);
            }

            Console.WriteLine("");
            Console.Write("    ");

            for (int i = 0; i < 21; i++)
            {
                Console.Write("-");
            }

            Console.WriteLine("");

            for (int i = 0; i < 5; i++)
            {
                for(int j=0;j<13;j++)
                {
                    if(2<=j && j<=11)
                    {
                        if(gameBoard[i,j-2]==0)
                        {
                            Console.Write("? ");
                        }
                        else
                        {
                            if(arrayOfMines[i,j-2]==1)
                            {
                                Console.Write("* ");
                            }
                            else
                            {
                                if (open[i, j - 2] == 1)
                                {
                                    Console.Write("{0} ", CountNeighborcell(i, j - 2));
                                }
                                else 
                                {
                                    Console.Write("- ");
                                }
                            }
                        }
                    }
                    if (j == 1 || j == 12)
                    {
                        Console.Write("| ");
                    }
                    if (j == 0)
                    {
                        Console.Write("{0} ", i);
                    }
                }
                Console.WriteLine("");
            }

            Console.Write("    ");

            for (int i = 0; i < 21; i++)
            {
                Console.Write("-");
            }

            Console.WriteLine("");
        }

        private static int CountNeighborcell(int i,int j)
        {
            int counter=0;
            
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

        private static void DisplayTop()
        {
            Console.WriteLine("Scoreboard: {0}", Environment.NewLine);
            for (int i = 0; i < playerScoreCounter%6; i++)
            {
                Console.WriteLine("{0}. {1} --> {2} cells",i,playerName[i],playerScore[i]);
            }
        }

        private static bool CheckBoardDimensions(int i,int j)
        {
            return ( 0<=i && i<=4) && (0<=j && j<=9);
        }

        private static int CountOpen()
        {
            int result = 0;
            for(int i=0;i<5;i++)
                for (int j = 0; j < 10; j++)
                {
                    if (open[i, j] == 1)
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

            //DisplayArrayOfMines();

            //TO DO: Some REFACTORING Used for go to !!!
            begin:
           
            Console.WriteLine("Welcome to the game “Minesweeper”.{0} Try to reveal all cells without mines. Use 'TOP' to view the scoreboard, {0}'RESTART' to start a new game and 'EXIT' to quit the game.", Environment.NewLine);
        
            InitializeArrayOfMines();

            //TO DO:Refactoring with While!!!
             f:
            Console.WriteLine("{0} Please input your move: ",Environment.NewLine);
            string playersMove = Console.ReadLine();

            //TO DO: Must be refactored!!!
            if (playersMove.Equals("restart"))
            { 
                goto begin; 
            }

            if (playersMove.Equals("top"))
            { 
                DisplayTop(); 
                goto begin; 
            }

            if (playersMove.Equals("exit"))
            {
                goto end;
            }

            // MAIN
            if (playersMove.Length < 3)
            { 
                Console.WriteLine("Illegal input");
                goto f;
            }

            int moveToRow = Convert.ToInt32((playersMove.ElementAt(0)).ToString());

            int moveToColumn = Convert.ToInt32((playersMove.ElementAt(2)).ToString());

            Console.WriteLine(moveToRow);
           
            if (open[moveToRow, moveToColumn] == 1)
            { 
                Console.WriteLine("Illegal move!"); 
                goto f;
            }

            if (open[moveToRow, moveToColumn] == 0)
            {
                open[moveToRow, moveToColumn] = 1;
                gameBoard[moveToRow, moveToColumn] = 1;

                if (arrayOfMines[moveToRow, moveToColumn] == 1)
                {
                    for (int i = 0; i < 5; i++)
                        for (int j = 0; j < 10; j++)
                        { 
                            gameBoard[i, j] = 1;
                        }

                        DisplayArrayOfMines();
                        Console.WriteLine("Booooom! You were killed by a mine. {0} You score is {1}. Please enter your name for the top scoreboard: ", Environment.NewLine, playerScoreCounter);

                    string name = Console.ReadLine();

                    playerName[playerScoreCounter % 5] = name;
                    playerScore[playerScoreCounter % 5] = CountOpen() - 1;

                    goto begin;

                }

                Console.WriteLine(CountNeighborcell(moveToRow, moveToColumn));

                DisplayArrayOfMines();

                goto f;

            }
            Console.WriteLine();

            end:

            Console.WriteLine("Good Bye!!!");
        }
    }
}