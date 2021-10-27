
namespace CongestionSample.Vehicles
{
    public class Van : IVehicle
    {
        private readonly double _amCharge;
        private readonly double _pmCharge;

        public Van()
        {

        }

        public Van(double amCharge, double pmCharge)
        {
            _amCharge = amCharge;
            _pmCharge = pmCharge;
        }

        public (double amCharge, double pmCharge) VehicleCharge()
        {
            var amCharge = _amCharge;
            var pmCharge = _pmCharge;

            return (amCharge, pmCharge);
        }
    }
}
