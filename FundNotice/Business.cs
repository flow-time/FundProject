using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


    }
}
