using System;

namespace Pitstop.RentalManagementAPI.Extensions;

public static class DateTimeExtensions
{
    public static bool IsBefore(this DateTime date, DateTime other)
    {
        return date < other;
    }
    
    public static bool IsBetween(this DateTime date, DateTime start, DateTime end)
    {
        return date >= start && date <= end;
    }
}