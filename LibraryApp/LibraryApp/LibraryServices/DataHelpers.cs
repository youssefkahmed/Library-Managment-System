using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryServices
{
    public class DataHelpers
    {
        public static List<string> HumanizeBusinessHours(IEnumerable<BranchHours> branchHours) 
        {
            var hours = new List<string>();

            foreach (var branchHour in branchHours)
            {
                var day = HumanizeDay(branchHour.DayOfWeek);
                var openTime = HumanizeTime(branchHour.OpenTime);
                var closeTime = HumanizeTime(branchHour.CloseTime);

                string time = $"{day}: {openTime} to {closeTime}";
                hours.Add(time);
            }

            return hours;
        }

        public static string HumanizeTime(int time)
        {
            return TimeSpan.FromHours(time).ToString("hh' : 'mm");
        }

        public static string HumanizeDay(int dayOfWeek)
        {
            return Enum.GetName(typeof(DayOfWeek), dayOfWeek - 1);
        }
    }
}
