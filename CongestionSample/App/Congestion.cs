using System;
using System.Collections.Generic;

namespace CongestionSample.App.App
{
    public class Congestion
    {
        private readonly TimeSpan _am;
        private readonly TimeSpan _pm;
        private readonly TimeSpan _noon;
        private readonly DateTime _from;
        private readonly DateTime _to;

        public Congestion()
        {

        }

        public Congestion(TimeSpan am, TimeSpan pm, TimeSpan noon, DateTime from, DateTime to)
        {
            _am = am;
            _pm = pm;
            _noon = noon;

            _from = from;
            _to = to;
        }

        public (double amTotal, double pmTotal) ChargePeriodTotal()
        {
            var amTotal = (_noon - _am).TotalMinutes;
            var pmTotal = (_pm - _noon).TotalMinutes;

            return (amTotal, pmTotal);
        }

        public double ContinuousDays()
        {
            var weekEnds = Week().weekEnds.Count;

            var fullDays = Math.Round((_to - _from).TotalDays, MidpointRounding.ToZero);

            if (weekEnds > 0) { return fullDays - weekEnds; }

            return fullDays;
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

        public (double amTime, double pmTime) TimeCharged(double continuousDays, double amTotal, double pmTotal)
        {
            double timeAM = 0;
            double timePM = 0;

            var startHour = _from.TimeOfDay;
            var endHour = _to.TimeOfDay;

            if (startHour < _am) { startHour = _am; }
            if (endHour > _pm) { endHour = _pm; }

            if (startHour <= _noon)
            {
                if (endHour >= _noon)
                { timeAM = (_noon - startHour).TotalMinutes; }

                else if (startHour > endHour)
                { timeAM = (_noon - startHour + endHour - _am).TotalMinutes; }

                else { timeAM = (endHour - startHour).TotalMinutes; }
            }

            if (endHour >= _noon)
            {
                if (startHour < _noon) { timePM = (endHour - _noon).TotalMinutes; }

                else if (startHour > endHour)
                {
                    timeAM = (_noon - _am).TotalMinutes;
                    timePM = (_pm - startHour + endHour - _noon).TotalMinutes;
                }

                else
                { timePM = (endHour - startHour).TotalMinutes; }
            }

            if (endHour <= _noon)
            {
                if (startHour <= _noon)
                {
                    timeAM = (endHour - _am + _noon - startHour).TotalMinutes;
                    timePM = (_pm - _noon).TotalMinutes;
                }

                if (startHour >= _noon)
                {
                    timeAM = (endHour - _am).TotalMinutes;
                    timePM = (_pm - startHour).TotalMinutes;
                }
            }

            if (startHour == endHour)
            {
                timeAM = 0;
                timePM = 0;
            }

            timeAM = timeAM + continuousDays * amTotal;
            timePM = timePM + continuousDays * pmTotal;

            return (timeAM, timePM);
        }
    }
}
