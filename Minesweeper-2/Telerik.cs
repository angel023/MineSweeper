using System;
using System.Collections.Generic;
using System.Linq;gameField
using System.Text;

// Initial commit!

namespace Minesweeper
{
    class Telerik
    {
        private static int[,] arrayOfMines=new int[5,10];
        private static int[] randomNumber = new int[15];
        private static int[,] gameField= new int[5,10];
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

        private static void InitializearrayOfMines()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    arrayOfMines[i, j] = 0;
                    gameField[i, j] = 0;
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

               // Console.WriteLine("{0},{1},{2}",index,(index/10),(index % 10));
                randomNumber[i] = index;
               


                arrayOfMines[(index / 10), (index % 10)] = 1;
                
            }
        }

        private static void DisplayarrayOfMines()
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
                        if(gameField[i,j-2]==0)
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
                                if (open[i, j - 2] == 1) Console.Write("{0} ", CountNeighborcell(i, j - 2));
                                else Console.Write("- ");
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
                    if (proverka(i + i1, j + j1) && arrayOfMines[i + i1, j + j1] == 1)
                    {
                        counter++;
                    }
                }
            }
            
            return counter;
        }

        /*\
         * 
         * 
         * 
         * 
         **********************
         **
         *****
         *
         * 
         *  tui e nqkvo krasivo
         * krasotata e hubavo neshto...
         * 
         * 
         * 
         */

        private static void DisplayTop()
        {
            Console.WriteLine("Scoreboard:\n");
            for (int i = 0; i < (playerScoreCounter)%6; i++)
            {
                Console.WriteLine("{0}. {1} --> {2} cells",i,playerName[i],playerScore[i]);
            }
        }

        private static bool proverka(int i,int j)
        {
            return ( 0<=i && i<=4) && (0<=j && j<=9);
        }

        private static int CountOpen()
        {
            int res = 0;
            for(int i=0;i<5;i++)
                for (int j = 0; j < 10; j++)
                {
                    if (open[i, j] == 1)
                        res++;
                }
            return res;
        }

        static void Main(string[] argumenti)
        {
            //InitializearrayOfMines();
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
            //gameField[1, 3] = 1;
            //gameField[2, 4] = 1;
            //gameField[0, 0] = 1;
            //gameField[2, 1] = 1;
            //gameField[1, 0] = 1;

            //DisplayarrayOfMines();
            begin:
            // tuka skachame kogaot iskame da begin-nem

            Console.WriteLine("Welcome to the game “Minesweeper”.\nTry to reveal all cells without mines. Use 'top' to view the scoreboard,\n'restart' to start a new game and 'exit' to quit the game.");
        
            InitializearrayOfMines();
  //tui "f:" e adski qko a?
            f:
            Console.WriteLine("\nEnter row and column: ");
            string p = (Console.ReadLine());

            if (p.Equals("restart"))
            { goto begin; }

            if (p.Equals("top"))
            { DisplayTop(); goto begin; }

            if (p.Equals("exit"))
                goto end;

            // MAIN
            if (p.Length < 3)
            { Console.WriteLine("Illegal input"); goto f; }
            int p1 = Convert.ToInt32((p.ElementAt(0)).ToString());

            int p2 = Convert.ToInt32((p.ElementAt(2)).ToString());
            Console.WriteLine(p1);
           
            if (open[p1, p2] == 1)
            { Console.WriteLine("Illegal move!"); goto f; }

            if (open[p1, p2] == 0)
            {
                open[p1, p2] = 1;
                gameField[p1, p2] = 1;
                if (arrayOfMines[p1, p2] == 1)
                {
                    for (int i = 0; i < 5; i++)
                        for (int j = 0; j < 10; j++)
                        { gameField[i, j] = 1; }
                    DisplayarrayOfMines();
                    Console.WriteLine("Booooom! You were killed by a mine. You revealed 2 cells without mines.Please enter your name for the top scoreboard:");
                    string str = Console.ReadLine();
                    playerName[playerScoreCounter % 5] = str;
                    playerScore[playerScoreCounter % 5] = CountOpen() - 1;
                    goto begin;
                }
                Console.WriteLine(CountNeighborcell(p1, p2));
                DisplayarrayOfMines();
                goto f;
            }



            //Console.WriteLine(w==q);
            Console.WriteLine();

        end:
            Console.WriteLine("Good Bye");
        }
    }
}
