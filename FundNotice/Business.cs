using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FundNotice
{
    public static class Business
    {
        //转换百分比
        public static float CalculatePercent(float todayWorth, float lastBuyWorth)
        {
            float difference = Math.Abs(todayWorth - lastBuyWorth);
            float percent = difference / lastBuyWorth;
            var convert = percent.ToString("P2");
            return percent;
        }

        public static bool InsertFundInfoToDatabase(FundTodayInfo fundTodayInfo)
        {
            string existSql = @"select COUNT(*) FROM FundsInfo WHERE fundId = @fundId;";
            object existBody = new { fundId = fundTodayInfo.fundcode, };

            int exist = SqlServerHelper.ExcuteSclacr<int>(existSql, existBody);
            if (exist > 0)
            {
                return true;
            }


            string sqlCommand = @"INSERT INTO [dbo].[FundsInfo]
(--Columns to insert data into
    [fundId], [createDate],[fundName]
)
VALUES
(--First row: values for the columns in the list above
 @fundId, @createDate, @fundName
)
                ";

            object body = new
            {
                fundId = fundTodayInfo.fundcode,
                createDate = DateTime.Now,
                fundName = fundTodayInfo.name
            };
            return SqlServerHelper.InsertDataToSqlServer(sqlCommand, body) > 0;
        }

        public static float GetNetWortByFixedDate(string content)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(content);

            int i = 0;
            float netWorthVal = 0f;
            foreach (var item in htmlDocument.DocumentNode.SelectNodes("//table"))
            {
                var tableId = item.Id;
                foreach (var item2 in item.SelectNodes("//thead"))
                {
                    foreach (HtmlNode cell in item2.SelectNodes(".//tr"))
                    {
                        foreach (HtmlNode childCell in cell.SelectNodes("th"))
                        {
                            i++;
                            if (childCell.InnerText.Equals("单位净值"))
                            {
                                break;
                            }
                        }
                        break;
                    }
                    break;
                }
                foreach (HtmlNode bodyItem in item.SelectNodes("//tbody"))
                {
                    foreach (HtmlNode bodytrItem in bodyItem.SelectNodes(".//tr"))
                    {
                        netWorthVal = float.Parse(bodytrItem.SelectNodes("th|td")[i - 1].InnerText);
                        break;
                    }
                }
            }
            return netWorthVal;
        }

    }
}
