using System;

namespace CongestionSample.Time
{
    public class ChargePeriod
    {
        private readonly TimeSpan _amStart;
        private readonly TimeSpan _pmEnd;
        private readonly TimeSpan _priceShift;

        public ChargePeriod(TimeSpan amStart, TimeSpan pmEnd, TimeSpan priceShift)
        {
            _amStart = amStart;
            _pmEnd = pmEnd;
            _priceShift = priceShift;
        }

        public (TimeSpan amStart, TimeSpan pmEnd, TimeSpan priceShift) CongestionPeriod()
        {
            var amStart = _amStart;
            var pmEnd = _pmEnd;
            var priceShift = _priceShift;

            return (amStart, pmEnd, priceShift);
        }
    }
}
