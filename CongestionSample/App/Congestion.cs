using CongestionSample.Time;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CongestionSample.App

{
    public class Congestion
    {
        private readonly DateAndTime _dateAndTime;
        private readonly TimeSpan _am = TimeSpan.Zero;
        private readonly TimeSpan _pm = TimeSpan.Zero;
        private readonly TimeSpan _noon = TimeSpan.Zero;

        public Congestion()
        {

        }

        public Congestion((TimeSpan am, TimeSpan pm, TimeSpan noon) period, DateAndTime dateAndTime)
        {
            _am = period.am;
            _pm = period.pm;
            _noon = period.noon;
            _dateAndTime = dateAndTime;
        }

        public double ContinuousDays()
        {
            var weekEnds = _dateAndTime.Week().weekEnds.Count();
            var from = _dateAndTime.From();
            var to = _dateAndTime.To();

            var fullDays = Math.Round((to - from).TotalDays, MidpointRounding.ToZero);

            if (weekEnds > 0) { return fullDays - weekEnds; }

            return fullDays;
        }

        public (TimeSpan amTotal, TimeSpan pmTotal) ChargePeriod()
        {
            var amTotal = _noon - _am;
            var pmTotal = _pm - _noon;

            return (amTotal, pmTotal);
        }

        public (double amTime, double pmTime) TimeSpent()
        {
            double timeAM = 0;
            double timePM = 0;

            var startHour = _dateAndTime.Week().weekDays.First().TimeOfDay;
            var endHour = _dateAndTime.Week().weekDays.Last().TimeOfDay;

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

            return (timeAM, timePM);
        }
    }
}
