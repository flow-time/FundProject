using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundNotice
{
    public static class HttpClientHelper
    {
        /*参数描述
         * fundId: 基金代码
         * startDate: 起始时间
         * endDate: 最终时间
         */

        //获取基金当天净值
        public static string FundTodayWorth(string fundId)
        {
            using HttpClient httpClient = new();
            var res = httpClient.GetAsync($"https://fundgz.1234567.com.cn/js/{fundId}.js").GetAwaiter().GetResult();
            var todayWorthRes = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return todayWorthRes;
        }

        //获取基金历史净值
        public static string FundHistoryWorth(string fundId, DateTime startDate, DateTime endData)
        {
            string startDateStr = startDate.ToString("yyyy-MM-dd"),
                endDateStr = endData.ToString("yyyy-MM-dd");
            using HttpClient httpClient = new();
            var res = httpClient.GetAsync(@$"http://fund.eastmoney.com/f10/F10DataApi.aspx?type=lsjz&code={fundId}&page=1&per=20&sdate={startDateStr}&edate={endDateStr}").GetAwaiter().GetResult();

            return res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }

    }

    public class FundTodayInfo
    {
        /// <summary>
        /// 基金代码
        /// </summary>
        public string fundcode { get; set; }
        /// <summary>
        /// 基金名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 净值日期
        /// </summary>
        public string jzrq { get; set; }
        public string dwjz { get; set; }
        /// <summary>
        /// 净值估算
        /// </summary>
        public string gsz { get; set; }
        /// <summary>
        /// 估算涨幅
        /// </summary>
        public string gszzl { get; set; }
        /// <summary>
        /// 估值时间
        /// </summary>
        public string gztime { get; set; }
    }

    public class FundHistoryWorth
    {
        public string content { get; set; }
        public int records { get; set; }
        public int pages { get; set; }
        public int curpage { get; set; }
    }

    public class FundTodayEntity
    {
        public string fundcode { get; set; }
        public string name { get; set; }
        public string jzrq { get; set; }
        public string dwjz { get; set; }
        public string gsz { get; set; }
        public string gszzl { get; set; }
        public string gztime { get; set; }
    }

}
