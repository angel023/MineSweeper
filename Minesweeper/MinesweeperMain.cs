using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinesweeperProject
{
    /// <summary>
    /// In this class we have Minesweeper game logic 
    /// </summary>
    public class MinesweeperMain
    {
        private static GameField gameField = new GameField(5, 10);
        private static Player player = new Player();
        private static List<Player> topPlayers = new List<Player>();

        /// <summary>
        /// This method works with the other classes and methods to control the game logic 
        /// </summary>
        public static void Main()
        {
            StartNewGame();
            while (player.Move != "exit")
            {
                Console.WriteLine("{0} Please input your move: ", Environment.NewLine);
                player.Move = Console.ReadLine().ToLower();

                if (player.Move == "top" || player.Move == "restart" || player.Move == "exit")
                {
                    if (player.Move == "top")
                    {
                        DisplayScoreBoard();
                    }

                    if (player.Move == "restart")
                    {
                        StartNewGame();
                    }

                    if (player.Move == "exit") // For the ability to exit at the beginning
                    {
                        Console.WriteLine("Good Bye!!!");
                        return;
                    }
                }
                else if (IsMoveInputLegal(player.Move))
                {
                    int moveToRow = int.Parse(player.Move[0].ToString());
                    int moveToColumn = int.Parse(player.Move[2].ToString());

                    if (gameField.IsCellOpen(moveToRow, moveToColumn))
                    {
                        gameField.OpenCells[moveToRow, moveToColumn] = 1;
                        gameField.Field[moveToRow, moveToColumn] = 1;

                        if (gameField.ArrayOfMines[moveToRow, moveToColumn] == 1)
                        {
                            gameField.RevealGameField();
                            Console.WriteLine(gameField);
                            Console.WriteLine(gameField.ToString());
                            Console.WriteLine("You were killed by a mine. {0} You score is {1}. Please enter your name for the top scoreboard: ", Environment.NewLine, player.Score);
                            EnterPlayerResult();
                            player = new Player();
                            gameField.InitializeNewGameField();
                            Console.WriteLine(gameField);
                        }
                        else
                        {
                            player.Score++; //Console.WriteLine(gameField.CountSurroundingMines(moveToRow, moveToColumn));
                            Console.WriteLine(gameField);
                        }
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Good Bye!!!");
        }


        private static void DisplayScoreBoard()
        {
            Console.WriteLine("Scoreboard: {0}", Environment.NewLine);
            for (int i = 0; i < topPlayers.Count; i++)
            {
                Console.WriteLine("{0}. {1} --> {2} cells", i, topPlayers[i].Name, topPlayers[i].Score);
            }
        }

        /// <summary>
        /// Method find the best 5 TopScore PLayers and sort them in Descending Order
        /// </summary>
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

        /// <summary>
        /// Initial Starting Message
        /// </summary>
        private static void StartNewGame()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("---                                            ---");
            Console.WriteLine("---      Welcome to the game “Minesweeper”.    ---");
            Console.WriteLine("---    Try to reveal all cells without mines.  ---");
            Console.WriteLine("---    Write 'TOP' to view the scoreboard,     ---");
            Console.WriteLine("---       'RESTART' to start a new game        ---");
            Console.WriteLine("---         and 'EXIT' to quit the game.       ---");
            Console.WriteLine("---                                            ---");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine();
            gameField.InitializeNewGameField();
            Console.WriteLine(gameField);
        }

        /// <summary>
        /// Method for validate the Players moves
        /// </summary>
        /// <param name="moveInput">string parameters</param>
        /// <returns>true for valid move false for invalid one</returns>
        private static bool IsMoveInputLegal(string moveInput)
        {
            try
            {
                int moveToInt;
                if (moveInput.Length != 3 || !int.TryParse(moveInput[0].ToString(), out moveToInt) ||
                moveInput[1].ToString() != " " || !int.TryParse(moveInput[2].ToString(), out moveToInt))
                {
                    throw new FormatException("Incorrect format! The correct one is <number><space><number>!");
                }
                if (int.Parse(moveInput[0].ToString()) >= gameField.Field.GetLength(0))
                {
                    throw new ArgumentOutOfRangeException("You went out of the field!");
                }
                return true;
            }
            catch (FormatException e)
            {

                Console.WriteLine(e.Message);

                return false;
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}