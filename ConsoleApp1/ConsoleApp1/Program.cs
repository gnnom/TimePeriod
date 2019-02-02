using System;
using System.Collections.Generic;
//using System.Linq; = NO
using System.Text;
using System.Threading.Tasks;

namespace TimePeriodAnonymous
{
    // class Program
    // {
    //     static void Main(string[] args)
    //     {
    //
    //     }
    // }

    /// <summary>
    /// Struktura odcinku w czasie
    /// </summary>
    public struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        private readonly long totalSeconds;
        public sbyte Seconds => (sbyte)(Math.Abs(totalSeconds % 60));
        public sbyte Minutes => (sbyte)(Math.Abs(totalSeconds / 60 % 60));
        public short Hours => (short)(totalSeconds / 60 / 60);
        public long TotalSeconds => totalSeconds;

        /// <param name="hours">Liczba godzin</param>
        /// <param name="minutes">Liczba minut</param>
        /// <param name="seconds">Liczba sekund</param>
        public TimePeriod(short hours, sbyte minutes, sbyte seconds)
        {
            if (minutes > 59 || minutes < 0 || seconds > 59 || seconds < 0)
                throw new ArgumentException("Invalid input data");

            totalSeconds = seconds;
            totalSeconds += minutes * 60;
            totalSeconds += hours * 60 * 60;
        }
        /// <param name="hours">Liczba godzin</param> // OFF
        /// <param name="minutes">Liczba minut</param> // OFF
        public TimePeriod(short hours, sbyte minutes)
        {
            if (minutes > 59 || minutes < 0)
                throw new ArgumentException("Invalid input data");
            totalSeconds = minutes * 60;
            totalSeconds += hours * 60 * 60;
        }

        /// <param name="seconds">Calkowita ilosc sekund</param> // not parm ! OFF THIS
        public TimePeriod(long seconds)
        {
            totalSeconds = seconds;
        }
        /// <param name="timePeriod">Odcinek czasu w postaci tekstu (000:00:00)</param> // must
        public TimePeriod(string timePeriod)
        {
            var parts = timePeriod.Split(':');

            try
            {
                var seconds = byte.Parse(parts[2]);
                var minutes = byte.Parse(parts[1]);
                var hours = short.Parse(parts[0]);

                if (minutes > 59 || minutes < 0 || seconds > 59 || seconds < 0)
                    throw new ArgumentException("Invalid input data");

                totalSeconds = seconds;
                totalSeconds += minutes * 60;
                totalSeconds += hours * 60 * 60;
            }
            catch (Exception) // catch - on - nie zapomnij o tym
            {
                throw new ArgumentException($"Invalid input data: {timePeriod}");
            }
        }

        /// <param name="t1">Czas</param> // <===
        /// <param name="t2">Czas</param> // <===
        public TimePeriod(Time t1, Time t2)
        {
            totalSeconds = t2.TotalSeconds - t1.TotalSeconds;
        }


        public override string ToString()
        {
            if (totalSeconds < 0)
                return $"-{Hours:000}:{Minutes:00}:{Seconds:00}";
            return $"{Hours:000}:{Minutes:00}:{Seconds:00}";
        }

        public static bool operator <(TimePeriod a, TimePeriod b) => a.totalSeconds < b.totalSeconds;
        public static bool operator >(TimePeriod a, TimePeriod b) => a.totalSeconds > b.totalSeconds;
        public static bool operator <=(TimePeriod a, TimePeriod b) => a.totalSeconds <= b.totalSeconds;
        public static bool operator >=(TimePeriod a, TimePeriod b) => a.totalSeconds >= b.totalSeconds;
        public static bool operator ==(TimePeriod a, TimePeriod b) => a.Equals(b);
        public static bool operator !=(TimePeriod a, TimePeriod b) => !a.Equals(b);

        public bool Equals(TimePeriod other)
        {
            return this.totalSeconds == other.totalSeconds;
        }

        public override bool Equals(System.Object obj)
        {
            if (!(obj is TimePeriod))
                return false;

            var time = (TimePeriod)obj;

            return this.Equals(time);
        }

        public override int GetHashCode()
        {
            return this.totalSeconds.GetHashCode();
        }

        public static TimePeriod operator +(TimePeriod a, TimePeriod b)
        {
            var temp = a.totalSeconds + b.TotalSeconds;
            return new TimePeriod(temp);
        }

        public static TimePeriod operator -(TimePeriod a, TimePeriod b)
        {
            var temp = a.totalSeconds - b.TotalSeconds;
            return new TimePeriod(temp);
        }

        public int CompareTo(TimePeriod other)
        {
            if (this.totalSeconds < other.totalSeconds) return -1;
            else if (this.totalSeconds == other.totalSeconds) return 0;
            return 1;
        }
    }
}