using Data;
using System.Numerics;

namespace Testing
{
    [TestClass]
    public class DataTest
    {
        private BallAbstractApi? balls;
        private readonly Vector2 boardSize = new Vector2(800, 600);

        [TestMethod] public void BallTest()
        {
            balls = BallAbstractApi.CreateList(boardSize);
            Assert.AreEqual(boardSize, balls.BoardSize);
        }
    }
}