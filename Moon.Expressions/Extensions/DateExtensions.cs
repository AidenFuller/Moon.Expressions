using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Expressions.Extensions;

public static class DateExtensions
{
    public static bool IsBetween(this DateTime dateTime, DateTime start, DateTime end)
    {
        return dateTime >= start && dateTime <= end;
    }

    public static bool IsBetween(this DateOnly dateOnly, DateOnly start, DateOnly end)
    {
        return dateOnly >= start && dateOnly <= end;
    }
}
