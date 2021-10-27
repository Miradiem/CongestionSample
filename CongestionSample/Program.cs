using CongestionSample.App;
using CongestionSample.Time;
using CongestionSample.Vehicles;
using System;

namespace CongestionSample
{
    public class Program
    {
        static void Main(string[] args)
        {
            var am = new TimeSpan(07, 0, 0);
            var pm = new TimeSpan(19, 0, 0);
            var noon = new TimeSpan(12, 0, 0);

            var from = new DateTime(2008, 04, 24, 11, 32, 0);
            var to = new DateTime(2008, 04, 24, 14, 42, 0);

            var type = "Car";
            double amTariff = 2;
            double pmTariff = 2.5;

            var congestion = new Congestion(
                new ChargePeriod(am, pm, noon).CongestionPeriod(),
                new DateAndTime(from, to));
            var vehicle = new VehicleCharge(
                new VehicleType(type, amTariff, pmTariff).Type().VehicleCharge()); 
            var display = new CongestionDisplay();

            var commands = new CongestionCommand(vehicle, congestion, display);

            commands.Invoke();
        }
    }
}
