using CongestionSample.App.App;
using CongestionSample.App.Vehicles;
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

            var congestion = new Congestion(am, pm, noon, from, to);
            var vehicle = new Vehicle(type); 
            var display = new CongestionDisplay();

            var commands = new CongestionCommand(vehicle, congestion, display);

            commands.Invoke();
        }

        
    }
}
