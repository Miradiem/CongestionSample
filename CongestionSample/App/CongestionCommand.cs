using CongestionSample.Vehicles;

namespace CongestionSample.App
{
    public class CongestionCommand
    {
        private readonly Congestion _congestion;
        private readonly VehicleCharge _vehicle;
        private readonly CongestionDisplay _display;

        public CongestionCommand(
            VehicleCharge vehicle,
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
            var period = _congestion.ChargePeriod();
            var continousDays = _congestion.ContinuousDays();
            var timeSpent= _congestion.TimeSpent(continousDays, period.amTotal, period.pmTotal);

            var dayPrice = _vehicle.OneDayCharge(period.amTotal, period.pmTotal);
            var amCharge = _vehicle.ChargeAM(timeSpent.amTime);
            var pmCharge = _vehicle.ChargePM(timeSpent.pmTime);
            var totalCharge = _vehicle.TotalCharge(timeSpent.amTime, timeSpent.pmTime, continousDays, dayPrice);

            _display.Show(timeSpent.amTime, timeSpent.pmTime, amCharge, pmCharge, totalCharge);
        }
    }
}
