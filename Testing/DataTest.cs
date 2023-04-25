using Data;

namespace Testing
{
    [TestClass]
    public class DataTest
    {
        private BallAbstractApi ball;
        private IBall b1;

        [TestMethod] public void BallTest()
        {
            ball = BallAbstractApi.CreateList();
            b1 = BallAbstractApi.CreateBall(new System.Numerics.Vector2(4, 5));
            ball.Add(b1);
            Assert.AreEqual(1, ball.GetBallNumber());
            Assert.AreEqual(ball.Get(0), b1);
        }
    }
}