using System;

namespace CongestionSample.Vehicles
{
    public class VehicleCharge
    {
        private readonly double _amCharge;
        private readonly double _pmCharge;

        public VehicleCharge()
        {

        }

        public VehicleCharge((double amCharge, double pmCharge) vehicleCharge)
        {
            _amCharge = vehicleCharge.amCharge;
            _pmCharge = vehicleCharge.pmCharge;
        }

        public double OneDayCharge(double amTotal, double pmTotal)
         => amTotal / 60 * _amCharge +
            pmTotal / 60 * _pmCharge;

        public double TotalCharge(double timeSpentAM, double timeSpentPM, double continousDays, double oneDayPrice)
            => timeSpentAM / 60 * _amCharge +
               timeSpentPM / 60 * _pmCharge +
               continousDays * oneDayPrice;

        public double ChargeAM(double timeSpentAM)
           => timeSpentAM / 60 * _amCharge;

        public double ChargePM(double timeSpentPM)
          => timeSpentPM / 60 * _pmCharge;
    }
}
