using System;

namespace Coinbase_Portfolio_Tracker.Infrastructure
{
    public static class Utils
    {
        public static double CurrentUnixTimestamp()
        {
            return (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        }
    }
}