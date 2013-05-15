using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinesweeperProject
{
    class MinesweeperMain
    {
        private static GameField gameField = new GameField(5, 10);
        private static Player player = new Player();
        private static List<Player> topPlayers = new List<Player>();
        
        private static void DisplayScoreBoard()
        {
            Console.WriteLine("Scoreboard: {0}", Environment.NewLine);
            for (int i = 0; i < topPlayers.Count; i++)
            {
                Console.WriteLine("{0}. {1} --> {2} cells", i, topPlayers[i].Name, topPlayers[i].Score);
            }
        }
        
        private static void EnterPlayerResult()
        {
            player.Name = Console.ReadLine();
            topPlayers.Add(player);
            Console.WriteLine();

            if (topPlayers.Count > 5)
            {
                topPlayers.RemoveAt(5);
            }

            topPlayers = topPlayers.OrderByDescending(x => x.Score).ToList();

            if (topPlayers.Count > 1)
            {
                Console.WriteLine("Top Players:");
                for (int i = 0; i < topPlayers.Count; i++)
                {
                    Console.WriteLine(topPlayers[i].Name);
                }               
            }
            Console.WriteLine();
        }

        private static void StartNewGame()
        {
            Console.WriteLine("Welcome to the game “Minesweeper”.{0} Try to reveal all cells without mines. Use 'TOP' to view the scoreboard, {0}'RESTART' to start a new game and 'EXIT' to quit the game.", Environment.NewLine);
            Console.WriteLine();
            gameField.InitializeNewGameField();
            Console.WriteLine(gameField);
        }

        static void Main()
        {
            StartNewGame();
            string expected =
"    0 1 2 3 4 5 6 7 8 9  \n" +
"   --------------------- \n" +
"0 | ? ? ? ? ? ? ? ? ? ? | \n" +
"1 | ? ? ? ? ? ? ? ? ? ? | \n" +
"2 | ? ? ? ? ? ? ? ? ? ? | \n" +
"3 | ? ? ? ? ? ? ? ? ? ? | \n" +
"4 | ? ? ? ? ? ? ? ? ? ? | \n" +
"   ---------------------  ";
            Console.WriteLine(expected);
            Console.WriteLine(expected.Length);
            Console.WriteLine(gameField.ToString().Length);
            while (player.Move != "exit")
            {
                Console.WriteLine("{0} Please input your move: ", Environment.NewLine);
                player.Move = Console.ReadLine();

                int moveToRow;
                int moveToColumn;

                if (player.Move == "top" || player.Move == "restart")
                {
                    if (player.Move == "top")
                    {
                        DisplayScoreBoard();
                    }

                    StartNewGame();
                }

                else if (player.Move.Length == 3 && int.TryParse(player.Move[0].ToString(), out moveToRow)
                    && int.TryParse(player.Move[2].ToString(), out moveToColumn) && player.Move[1].ToString() == " ")
                {                                                                                                   
                    Console.WriteLine(moveToRow);

                    if (gameField.isCellOpen(moveToRow, moveToColumn))
                    {
                        gameField.OpenCells[moveToRow, moveToColumn] = 1;
                        gameField.Field[moveToRow, moveToColumn] = 1;
                        
                        if (gameField.ArrayOfMines[moveToRow, moveToColumn] == 1)
                        {   
                            gameField.RevealGameField();
                            Console.WriteLine(gameField);
                            Console.WriteLine("Booooom! You were killed by a mine. {0} You score is {1}."+
                                " Please enter your name for the top scoreboard: ", Environment.NewLine, player.Score);
                            EnterPlayerResult();
                            player = new Player();
                            gameField.InitializeNewGameField();
                            Console.WriteLine(gameField);
                        }
                        else
                        {
                            player.Score++;
                            //Console.WriteLine(gameField.CountSurroundingMines(moveToRow, moveToColumn));
                            Console.WriteLine(gameField);
                        }
                    }

                    else 
                    {
                        Console.WriteLine("Illegal move!");
                    }
                }
                else if (player.Move != "exit")
                {
                    Console.WriteLine("Illegal input");
                }
            }
            Console.WriteLine();

            Console.WriteLine("Good Bye!!!");
        }
    }
}