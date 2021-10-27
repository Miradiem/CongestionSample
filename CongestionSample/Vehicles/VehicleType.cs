
namespace CongestionSample.Vehicles
{
    public class VehicleType
    {
        private readonly string _type;
        private readonly double _amTariff;
        private readonly double _pmTariff;

        public VehicleType()
        {

        }

        public VehicleType(string type, double amTariff, double pmTariff)
        {
            _type = type;
            _amTariff = amTariff;
            _pmTariff = pmTariff;
        }

        public IVehicle Type()
        {
            switch (_type)
            {
                case "Car": return new Car(_amTariff, _pmTariff);
                case "Van": return new Van(_amTariff, _pmTariff);
                case "Motorbike": return new Motorbike(_amTariff, _pmTariff);
                default: return new Car(_amTariff, _pmTariff);
            }
        }
    }
}
