using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace LogicTests
{
    [TestClass]
    public class LogicTest
    {
        private BallLogicAPI ballLogic;
        private readonly Vector2 boardSize = new Vector2(200, 200);


        [TestMethod]
        public void AddBallTest()
        {
            ballLogic = BallLogicAPI.CreateBallsLogic(this.boardSize);
            ballLogic.AddBall(boardSize / 2);
            Assert.AreEqual(1, ballLogic.GetBallsNumber());
            Assert.AreEqual(boardSize / 2, ballLogic.GetBalls()[0].Position);

            //Remove
        }

        [TestMethod]
        public void AddBallsTest()
        {
            ballLogic = BallLogicAPI.CreateBallsLogic(this.boardSize);
            ballLogic.AddBalls(15);
            Assert.AreEqual(15, ballLogic.GetBallsNumber());
        }

        [TestMethod]
       
       public void SimulationTest()
        {
            
        }



    }
}
    
