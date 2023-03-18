using FundNotice;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;

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

        [TestMethod]
        public void Test_InsertToSqlServer()
        {
            SqlServerHelper.InsertDataToSqlServer(null, null);
        }

        [TestMethod]
        public void Test_GetDateA()
        {
            string str = "2023-2-2";
            string res = DateTime.Parse(str).ToString("yyyy-MM-dd");
        }

        [TestMethod]
        [DataRow("012414", "2023-3-17", "2023-3-17")]
        public void Test_GetHistoryWorth(string fundId, string startDateStr, string endDateStr)
        {
            //https://www.cnblogs.com/fishyues/p/10232822.html
            string res = HttpClientHelper.FundHistoryWorth(fundId, DateTime.Parse(startDateStr), DateTime.Parse(endDateStr));
            res = res.Replace("var apidata=", "").TrimEnd(';');
            var asdasd = JsonConvert.DeserializeObject<HistoryWorth>(res);

          
            Assert.IsNotNull(netWorthVal);
        }
    }
}