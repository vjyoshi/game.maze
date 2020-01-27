using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace game.maze.tests
{
    [TestClass]
    public class MazeTests
    {
        [TestMethod]
        private void GameCreationTest()
        {
            IGame sut = new Maze(10, 10);
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        private void ValidGameTest()
        {
            IGame sut = new Maze(10, 10);
            Cell cell = sut.GetStartingPosition();
            Assert.IsNotNull(cell);
        }        
    }
}
