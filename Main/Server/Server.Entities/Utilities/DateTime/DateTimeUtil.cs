﻿namespace Server.Util;

public static class DateTimeUtil
{
    public static DateTime FromUnixTime(int unixTime)
    {
        if (unixTime <= 0) return DateTime.Now;

        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return epoch.AddSeconds(unixTime);
    }

    public static DateTime FromUnixTime(long unixTime)
    {
        if (unixTime <= 0) return DateTime.Now;

        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return epoch.AddSeconds(unixTime);
    }

    public static int FromUnixTimeDaysPeriod(long unixTime)
    {
        return DateTime.Now.Date.Subtract(FromUnixTime(unixTime).Date).Duration().Days + 1;
    }

    public static int ToUnixTimeInt(DateTime date)
    {
        if (date == default) return 0;

        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return Convert.ToInt32((date - epoch).TotalSeconds);
    }

    public static long ToUnixTimeLong(DateTime date)
    {
        if (date == default) return 0;

        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return Convert.ToInt64((date - epoch).TotalSeconds);
    }
}
