using Data;

namespace Testing
{
    [TestClass]
    public class UnitTest1
    {
        DataAbstractAPI test;
        [TestMethod]
        public void BallTest()
        {
            test = DataAbstractAPI.CreateBall(1, 2);
            Assert.AreEqual(test.getPositionX(), 1);
            Assert.AreEqual(test.getPositionY(), 2);
        }
        [TestMethod]
        public void TestBallSetValues()
        {
            test = DataAbstractAPI.CreateBall(2, 3);
            Assert.AreEqual(test.getPositionX(), 2);
            Assert.AreEqual(test.getPositionY(), 3);
            test.setPositionX(5);
            test.setPositionY(10);
            Assert.AreEqual(test.getPositionX(), 5);
            Assert.AreEqual(test.getPositionY(), 10);
        }
    }
}