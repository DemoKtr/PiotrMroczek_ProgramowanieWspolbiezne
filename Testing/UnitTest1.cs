using TPW;
namespace Testing
{
    [TestClass]
    public class UnitTest1
    {
        Class1 klasa = new Class1();
        
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(klasa.add(1, 3), 4);

        }
        [TestMethod]
        public void TestMethod2()
        {
            Assert.AreEqual(klasa.subtract(3, 1), 2);

        }
        [TestMethod]
        public void TestMethod3()
        {
            Assert.AreEqual(klasa.multiply(1, 3), 3);

        }
        [TestMethod]
        public void TestMethod4()
        {
            Assert.AreEqual(klasa.divide(6, 2), 3);

        }
        [TestMethod]
        public void TestMethod5()
        {
            Assert.AreEqual(klasa.pow(2), 4);

        }
    }
}