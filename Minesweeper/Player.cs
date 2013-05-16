using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinesweeperProject
{
   public class Player
    {
        /// <summary>
        /// Gets or sets Score of each Player
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets Name of the player
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Moves of Player
        /// </summary>
        public string Move { get; set; }
        
        /// <summary>
        /// Initializes a new instance of the class Player
        /// as Default Constructor of Player Class initialize Move and Score
        /// </summary>
        public Player() 
        {
            this.Score = 0;
            this.Move = "";
        }
    }
}
