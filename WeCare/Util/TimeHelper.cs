using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeCare.Util
{
    public static class TimeHelper
    {
        public static DateTime getStartWeek(string AnyDate)
        {
            var eDay = DateTime.Parse(AnyDate);
            var startweek = new DateTime();
            if (eDay.DayOfWeek == DayOfWeek.Monday)
                startweek = new DateTime(eDay.Year, eDay.Month, eDay.Day);
            else
            {
                var DoW = (int)eDay.DayOfWeek;
                if (DoW == 0) DoW = 7;
                var tempdate = new DateTime();
                var difDay = 0;
                if (eDay.Day > DoW) difDay = Math.Abs(1 - DoW);
                else difDay = Math.Abs(DoW - 1);
                tempdate = eDay.AddDays(-difDay);
                startweek = new DateTime(tempdate.Year, tempdate.Month, tempdate.Day);
            }
            return startweek;
        }

        public static DateTime getEndWeek(DateTime startWeek)
        {
            var endWeek = startWeek.AddDays(6);
            return new DateTime(endWeek.Year, endWeek.Month, endWeek.Day, 23, 59, 59);
        }

        public static bool checkWeek(string AnyDate, DateTime startWeek, DateTime endWeek)
        {
            var eDay = DateTime.Parse(AnyDate);
            if (eDay >= startWeek && eDay <= endWeek)
            {
                return true;
            }
            else return false;
        }

        public static bool checkDay(string AnyDate, DateTime compare)
        {
            var eDay = DateTime.Parse(AnyDate);
            if (eDay.Day == compare.Day && eDay.Month == compare.Month &&
                eDay.Year == compare.Year) return true;
            return false;
        }
    }
}
