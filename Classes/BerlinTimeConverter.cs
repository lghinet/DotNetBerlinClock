using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace BerlinClock
{
    public class BerlinTimeConverter : ITimeConverter
    {
        const char YellowLight = 'Y';
        const char RedLight = 'R';
        const char LightOff = 'O';


        /// <summary>
        /// Converts a string time to a Berlin Clock string format
        /// </summary>
        /// <param name="aTime"></param>
        /// <returns></returns>
        public string convertTime(string aTime)
        {
            var timeOfDay = TimeOfDay.ParseExact(aTime);

            var sb = new StringBuilder();
            sb.AppendLine(HandleSeconds(timeOfDay.Seconds));
            sb.AppendLine(HandleHours(timeOfDay.Hours));
            sb.Append(HandleMinutes(timeOfDay.Minutes));

            return sb.ToString();
        }

        /// <summary>
        /// Converts seconds to berlin clock seconds format 
        /// </summary>
        /// <param name="seconds"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        internal string HandleSeconds(int seconds)
        {
            if (seconds < 0 || seconds > 59)
                throw new ArgumentOutOfRangeException(nameof(seconds), "invalid seconds");

            return (seconds % 2 == 0 ? YellowLight : LightOff).ToString();
        }

        /// <summary>
        /// Converts hours to berlin clock hours format
        /// </summary>
        /// <param name="hours"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        internal string HandleHours(int hours)
        {
            if (hours < 0 || hours > 24)
                throw new ArgumentOutOfRangeException(nameof(hours), "invalid hours");

            var r1 = hours / 5;
            var r2 = hours - r1 * 5;

            var sb = new StringBuilder();
            sb.Append(RedLight, r1);
            sb.Append(LightOff, 4 - r1);
            sb.AppendLine();
            sb.Append(RedLight, r2);
            sb.Append(LightOff, 4 - r2);

            return sb.ToString();
        }

        /// <summary>
        /// Converts minutes to berlin clock minutes format
        /// </summary>
        /// <param name="minutes"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        internal string HandleMinutes(int minutes)
        {
            if (minutes < 0 || minutes > 59)
                throw new ArgumentOutOfRangeException(nameof(minutes), "invalid minutes");

            var r1 = minutes / 5;
            var r2 = minutes - r1 * 5;

            var sb = new StringBuilder();

            for (var l = 1; l <= r1; l++)
                sb.Append(l % 3 == 0 ? RedLight : YellowLight);

            sb.Append(LightOff, 11 - r1);
            sb.AppendLine();
            sb.Append(YellowLight, r2);
            sb.Append(LightOff, 4 - r2);

            return sb.ToString();
        }
    }
}