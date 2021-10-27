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
            var timeSpentAM = _congestion.TimeSpent().amTime;
            var timeSpentPM = _congestion.TimeSpent().pmTime;
            var continousDays = _congestion.ContinuousDays();

            var period = _congestion.ChargePeriod();
            var dayPrice = _vehicle.OneDayCharge(period.amTotal, period.pmTotal);

            var amCharge = _vehicle.ChargeAM(timeSpentAM);
            var pmCharge = _vehicle.ChargePM(timeSpentPM);
            var totalCharge = _vehicle.TotalCharge(timeSpentAM, timeSpentPM, continousDays, dayPrice);

            _display.Show(timeSpentAM, timeSpentPM, amCharge, pmCharge, totalCharge);
        }
    }
}
