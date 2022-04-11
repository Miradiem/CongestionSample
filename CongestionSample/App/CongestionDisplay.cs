using System;

namespace CongestionSample.App.App
{
    public class CongestionDisplay
    {
        public void Show(double timeSpentAM, double timeSpentPM, double amCharge, double pmCharge, double totalCharge)
        {
            var amRounded = Math.Round(amCharge, 1, MidpointRounding.ToZero);
            var pmRounded = Math.Round(pmCharge, 1, MidpointRounding.ToZero);
            var totalRounded = Math.Round(totalCharge, 1, MidpointRounding.ToZero);

            var am = TimeSpan.FromMinutes(timeSpentAM).ToString(@"h'h 'm'm'");
            var pm = TimeSpan.FromMinutes(timeSpentPM).ToString(@"h'h 'm'm'");

            var output = ($"Charge for {am} (AM rate): \u00A3{amRounded:0.00}" + "\n" +
                          $"Charge for {pm} (PM rate): \u00A3{pmRounded:0.00}" + "\n" +
                          $"Total Charge: \u00A3{totalRounded:0.00}").Replace(',', '.');

            Console.WriteLine(output);
        }
    }
}
