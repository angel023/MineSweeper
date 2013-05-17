using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinesweeperProject;

namespace MinesweeperProjectTest
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void PlayerConstructorScoreTest()
        {
            Player player = new Player();
            int expectedScore = 0;

            Assert.AreEqual(expectedScore, player.Score);
        }

        [TestMethod]
        public void PlayerConstructorMoveTest() 
        {
            Player player = new Player();
            string expectedMove = "";

            Assert.AreEqual(expectedMove, player.Move);
        }
    }
}
