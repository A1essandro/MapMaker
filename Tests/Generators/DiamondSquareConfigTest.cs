using Microsoft.VisualStudio.TestTools.UnitTesting;
using Generators;
using System.Threading.Tasks;

namespace Tests.Generators
{

    [TestClass]
    public class DiamondSquareConfigTest
    {

        #region Context

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #endregion

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestSize()
        {
            var config = new DiamondSquareConfig(3);
            Assert.AreEqual(9, config.Size);
        }

        [TestMethod]
        public void TestRandomSeed()
        {
            var configEq1 = new DiamondSquareConfig(3, 1, new System.Random(5));
            var configEq2 = new DiamondSquareConfig(3, 1, new System.Random(5));
            var configNotEq = new DiamondSquareConfig(3, 1, new System.Random(10));

            var generatorEq1 = new DiamondSquare(configEq1);
            var generatorEq2 = new DiamondSquare(configEq2);
            var generatorNotEq = new DiamondSquare(configNotEq);

            var mapEq1 = generatorEq1.Generate();
            var mapEq2 = generatorEq2.Generate();
            var mapnotEq = generatorNotEq.Generate();

            Assert.AreEqual(mapEq2.GetAwaiter().GetResult()[1, 2],
                mapEq1.GetAwaiter().GetResult()[1, 2]);

            Assert.AreNotEqual(mapnotEq.GetAwaiter().GetResult()[1, 2],
                mapEq1.GetAwaiter().GetResult()[1, 2]);

            //Note: if you use one DiamondSquareConfig reference for different generators - it will not work:
            //var config = new DiamondSquareConfig(3, 1, new System.Random(5));
            //var generator1 = new DiamondSquare(config);
            //var generator2 = new DiamondSquare(config);
            // generator1.Generate()[x, y] != generator2.Generate()[x, y] !!!;
        }
    }
}
