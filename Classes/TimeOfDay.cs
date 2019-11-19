using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerlinClock
{
    public struct TimeOfDay : IEquatable<TimeOfDay>, IComparable<TimeOfDay>, IComparable
    {
        public int Hours { get; }
        public int Minutes { get; }
        public int Seconds { get; }

        public TimeOfDay(int hours, int minutes, int seconds)
        {
            if (hours < 0 || hours > 24)
                throw new ArgumentOutOfRangeException(nameof(hours), "invalid hours");
            Hours = hours;

            if (minutes < 0 || minutes > 59)
                throw new ArgumentOutOfRangeException(nameof(minutes), "invalid minutes");
            Minutes = minutes;

            if (seconds < 0 || seconds > 59)
                throw new ArgumentOutOfRangeException(nameof(seconds), "invalid seconds");
            Seconds = seconds;
        }

        /// <summary>
        /// Parses a string time (ex:12:33:22) into a TimeOfDay 
        /// </summary>
        /// <param name="aTime"></param>
        /// <returns></returns>
        /// <exception cref="System.OverflowException"></exception>
        /// <exception cref="System.FormatException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static TimeOfDay ParseExact(string aTime)
        {
            if (aTime == "24:00:00")
                return new TimeOfDay(24, 0, 0);

            var timeOfDay = TimeSpan.ParseExact(aTime, "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
            return new TimeOfDay(timeOfDay.Hours, timeOfDay.Minutes, timeOfDay.Seconds);
        }

        public int CompareTo(TimeOfDay other)
        {
            var hoursComparison = Hours.CompareTo(other.Hours);
            if (hoursComparison != 0) return hoursComparison;
            var minutesComparison = Minutes.CompareTo(other.Minutes);
            if (minutesComparison != 0) return minutesComparison;
            return Seconds.CompareTo(other.Seconds);
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            return obj is TimeOfDay other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(TimeOfDay)}");
        }

        public static bool operator <(TimeOfDay left, TimeOfDay right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator >(TimeOfDay left, TimeOfDay right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator <=(TimeOfDay left, TimeOfDay right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >=(TimeOfDay left, TimeOfDay right)
        {
            return left.CompareTo(right) >= 0;
        }


        public bool Equals(TimeOfDay other)
        {
            return Hours == other.Hours && Minutes == other.Minutes && Seconds == other.Seconds;
        }

        public override bool Equals(object obj)
        {
            return obj is TimeOfDay other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Hours;
                hashCode = (hashCode * 397) ^ Minutes;
                hashCode = (hashCode * 397) ^ Seconds;
                return hashCode;
            }
        }


        public static bool operator ==(TimeOfDay left, TimeOfDay right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TimeOfDay left, TimeOfDay right)
        {
            return !left.Equals(right);
        }
    }
}