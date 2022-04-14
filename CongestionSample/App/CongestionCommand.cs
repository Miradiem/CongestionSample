using CongestionSample.App.Vehicles;

namespace CongestionSample.App.App
{
    public class CongestionCommand
    {
        private readonly Congestion _congestion;
        private readonly Vehicle _vehicle;
        private readonly CongestionDisplay _display;

        public CongestionCommand(
            Vehicle vehicle,
            Congestion congestion,
            CongestionDisplay display
            )
        {
            _vehicle = vehicle;
            _congestion = congestion;
            _display = display;
        }

        public void Invoke()
        {
            var continuousDays = _congestion.ContinuousDays();
            var period = _congestion.ChargePeriod();
            var timeSpent= _congestion.TimeCharged(continuousDays, period);

            var dayPrice = _vehicle.OneDayPrice(period.amTotal, period.pmTotal);
            var amCharge = _vehicle.PriceForAM(timeSpent.amTime);
            var pmCharge = _vehicle.PriceForPM(timeSpent.pmTime);
            var totalCharge = _vehicle.TotalPrice(timeSpent.amTime, timeSpent.pmTime, continuousDays, dayPrice);

            _display.Show(timeSpent.amTime, timeSpent.pmTime, amCharge, pmCharge, totalCharge);
        }
    }
}
