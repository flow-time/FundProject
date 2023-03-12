using FundNotice;

namespace FundNotice_Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_FundHistoryWorth()
        {
            //HttpClientHelper.FundHisttoryWorth();
            //Assert.IsNotNull(res);
        }

        [TestMethod]
        public void Test_Calcuate()
        {
            Business.CalculatePercent(1.1937f, 1.2388f);
        }
    }
}