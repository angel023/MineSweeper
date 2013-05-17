using System;
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
    }
}
