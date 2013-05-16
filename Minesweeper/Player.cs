using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinesweeperProject
{
   public class Player
    {
        public int Score { get; set; }
        public string Name { get; set; }
        public string Move { get; set; }
        
        /// <summary>
        /// Default Constructor of Player Class - initialize Move and Score
        /// </summary>
        public Player() 
        {
            this.Score = 0;
            this.Move = "";
        }
    }
}
