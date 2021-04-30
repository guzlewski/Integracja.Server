using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Models.Shared.Time
{
    public static class TimeHelper
    {
        public static string ReadableSeconds(int seconds)
        {
            TimeSpan span = new TimeSpan(0, 0, seconds);
            return ReadableTimeSpan(span, useSeconds: true);
        }

        public static string ReadableTicks(long ticks, bool useSeconds )
        {
            TimeSpan span = new TimeSpan(ticks);
            return ReadableTimeSpan(span, useSeconds);
        }

        public static string ReadableTimeSpan(TimeSpan duration, bool useSeconds = false)
        {
            int days = duration.Days;
            int hours = duration.Hours;
            int minutes = duration.Minutes;
            int seconds = duration.Seconds;

            var output = "";
            if (days > 0)
            {
                output = days + "d " + hours + "g " + minutes + "m";
            }
            else if (hours > 0)
            {
                output = hours + "g " + minutes + "m";
            }
            else if (minutes > 0)
            {
                output = minutes + "m";
            }

            if( useSeconds )
            {
                if (output != "")
                    output += " ";
                output += seconds + "s";
            }

            if( output == "" )
                output = "error: " + nameof(ReadableTimeSpan) + "()";

            return output;
        }

        public static double ToEpochMiliseconds( DateTime t )
        {
            return t.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

    }
}
