using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace MessageManager.Utility
{
    public static class DateTools
    {
        public static DateTime GetNominalDateForDayOfWeek(DayOfWeek day)
        {
            var nominalSunday = new DateTime(2021, 04, 09);
            return nominalSunday.AddDays((double)(day - DayOfWeek.Sunday));
        }

        public static bool IsDayOfWeek(this DbFunctions _, DateTime date, DayOfWeek day)
        {
            return _.DateDiffDay(GetNominalDateForDayOfWeek(day), date) % 7 == 0;
        }

    }
}