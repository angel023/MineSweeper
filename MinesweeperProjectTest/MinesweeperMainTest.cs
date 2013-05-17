using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinesweeperProject;

namespace MinesweeperProjectTest
{
    [TestClass]
    public class MinesweeperMainTest
    {
        [TestMethod]
        public void IsMoveInputLegalIncorrectInputWord()
        {
            PrivateType privateTypeObject = new PrivateType(typeof(MinesweeperMain));
            object obj = privateTypeObject.InvokeStatic("IsMoveInputLegal", "testWord");
            Assert.AreEqual(false, (bool)obj);
        }


        [TestMethod]
        public void IsMoveInputLegalTestLetterSpaceLetter()
        {
            PrivateType privateTypeObject = new PrivateType(typeof(MinesweeperMain));
            object obj = privateTypeObject.InvokeStatic("IsMoveInputLegal", "a b");
            Assert.AreEqual(false, (bool)obj);
        }


        [TestMethod]
        public void IsMoveInputLegalTestNumberSpaceLetter()
        {
            PrivateType privateTypeObject = new PrivateType(typeof(MinesweeperMain));
            object obj = privateTypeObject.InvokeStatic("IsMoveInputLegal", "5 b");
            Assert.AreEqual(false, (bool)obj);
        }

        [TestMethod]
        public void IsMoveInputLegalTestTwoDigitNumberSpaceLetter()
        {
            PrivateType privateTypeObject = new PrivateType(typeof(MinesweeperMain));
            object obj = privateTypeObject.InvokeStatic("IsMoveInputLegal", "10 b");
            Assert.AreEqual(false, (bool)obj);

        }

        [TestMethod]
        public void IsMoveInputLegalTestTwoDigitNumbers()
        {
            PrivateType privateTypeObject = new PrivateType(typeof(MinesweeperMain));
            object obj = privateTypeObject.InvokeStatic("IsMoveInputLegal", "15 10");
            Assert.AreEqual(false, (bool)obj);

        }

        [TestMethod]   
        public void IsMoveInputLegalTestOneDigitNumberLetter()
        {
            PrivateType privateTypeObject = new PrivateType(typeof(MinesweeperMain));
            object obj = privateTypeObject.InvokeStatic("IsMoveInputLegal", "1b");
            Assert.AreEqual(false, (bool)obj);

        }

        [TestMethod]
        public void IsMoveInputLegalTestFirstArg5()
        {
            PrivateType privateTypeObject = new PrivateType(typeof(MinesweeperMain));
            object obj = privateTypeObject.InvokeStatic("IsMoveInputLegal", "5 6");
            Assert.AreEqual(false, (bool)obj);
        }

        [TestMethod]
        public void IsMoveInputLegalTrueInput()
        {
            PrivateType privateTypeObject = new PrivateType(typeof(MinesweeperMain));
            object obj = privateTypeObject.InvokeStatic("IsMoveInputLegal", "1 2");

            Assert.AreEqual(true, (bool)obj);
        }

        [TestMethod]
        public void StartNewGameTest()
        {
            PrivateType mineSweeperMain = new PrivateType(typeof(MinesweeperMain));

            var currentConsoleOut = Console.Out;
            string expected = "--------------------------------------------------\r\n---" +
                "                                            ---\r\n---" +
                "      Welcome to the game “Minesweeper”.    ---\r\n---" +
                "    Try to reveal all cells without mines.  ---\r\n---" +
                "    Write 'TOP' to view the scoreboard,     ---\r\n---" +
                "       'RESTART' to start a new game        ---\r\n---" +
                "         and 'EXIT' to quit the game.       ---\r\n---" +
                "                                            " +
                "---\r\n--------------------------------------------------\r\n\r\n" +
                "    0 1 2 3 4 5 6 7 8 9 \r\n   ---------------------\r\n0 | ? ? ? ? ? ? ? ? ? ?" +
                " |\r\n1 | ? ? ? ? ? ? ? ? ? ? |\r\n2 | ? ? ? ? ? ? ? ? ? ? |" +
                "\r\n3 | ? ? ? ? ? ? ? ? ? ? |\r\n4 | ? ? ? ? ? ? ? ? ? ? |\r\n" +
                "   ---------------------\r\n\r\n";

            using (var consoleOutput = new ConsoleOutput())
            {
                mineSweeperMain.InvokeStatic("StartNewGame");
                string output = consoleOutput.GetOuput();

                Assert.AreEqual(expected, output);
            }
        }
    }
}
