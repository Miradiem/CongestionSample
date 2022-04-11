
namespace CongestionSample.App.Vehicles
{
    public class Vehicle
    {
        private readonly string _type;

        public Vehicle()
        {

        }

        public Vehicle(string vehicleType)
        {
            _type = vehicleType;
        }

        public double PriceForAM(double timeSpentAM)
          => timeSpentAM / 60 * VehicleType().amTariff;

        public double PriceForPM(double timeSpentPM)
          => timeSpentPM / 60 * VehicleType().pmTariff;

        public double PriceForDay(double amTotal, double pmTotal)
         => amTotal / 60 * VehicleType().amTariff +
            pmTotal / 60 * VehicleType().pmTariff;

        public double TotalCharge(double timeSpentAM, double timeSpentPM, double continousDays, double oneDayPrice)
            => timeSpentAM / 60 * VehicleType().amTariff +
               timeSpentPM / 60 * VehicleType().pmTariff +
               continousDays * oneDayPrice;

        public (double amTariff, double pmTariff) VehicleType()
        {
            switch (_type)
            {
                case "Car": return (2, 2.5);
                case "Van": return (2, 2.5);
                case "Motorbike": return (1, 1);
                default: return (0, 0);
            }
        }
    }
}
