using System;
using System.Collections.Generic;

namespace CongestionSample.Time
{
    public class DateAndTime
    {
        private readonly DateTime _from = new DateTime();
        private readonly DateTime _to = new DateTime();

        public DateAndTime(DateTime from, DateTime to)
        {
            _from = from;
            _to = to;
        }

        public DateTime From()
        {
            var from = _from;
            return from;
        }

        public DateTime To()
        {
            var to = _to;
            return to;
        }

        public (List<DateTime> weekDays, List<DateTime> weekEnds) Week()
        {
            var weekDays = new List<DateTime>();
            var weekEnds = new List<DateTime>();

            for (var dt = _from; dt <= _to; dt = dt.AddDays(1))
            {
                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
                { weekEnds.Add(dt); }

                else
                { weekDays.Add(dt); }
            }

            if (weekDays.Count == 1 && (_to.DayOfWeek != DayOfWeek.Saturday || _to.DayOfWeek != DayOfWeek.Sunday))
            { weekDays.Add(_to); }

            if (weekDays.Count >= 2)
            {
                if (_to.DayOfWeek == DayOfWeek.Saturday)
                {
                    weekDays.RemoveAt(weekDays.Count - 1);
                    weekDays.Add(_to.AddDays(-1));
                }

                if (_to.DayOfWeek == DayOfWeek.Sunday)
                {
                    weekDays.RemoveAt(weekDays.Count - 1);
                    weekDays.Add(_to.AddDays(-2));
                }

                else
                {
                    weekDays.RemoveAt(weekDays.Count - 1);
                    weekDays.Add(_to);
                }
            }

            return (weekDays, weekEnds);
        }
    }
}
