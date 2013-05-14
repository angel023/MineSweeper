using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinesweeperProject
{
    struct Player
    {
        public int Score { get; set; }
        public int Name { get; set; }
        public string Move { get; set; }
        
        public Player() 
        {
            this.Score = 0;
        }
    }
}
