using CongestionSample.App;
using CongestionSample.Time;
using CongestionSample.Vehicles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CongestionTests
{
    [TestClass]
    public class CongestionTest
    {
        [TestMethod]
        public void Test_WeekDay()
        {
            var from = new DateTime(2008, 04, 24, 11, 32, 0);
            var to = new DateTime(2008, 04, 24, 14, 42, 0);

            var week = new DateAndTime(from, to).Week().weekDays;
            var work = String.Join(" ", week);

            Assert.AreEqual(work, "2008-04-24 11:32:00 2008-04-24 14:42:00");
        }

        [TestMethod]
        public void Test_TimeSpent()
        {
            var am = new TimeSpan(07, 0, 0);
            var pm = new TimeSpan(19, 0, 0);
            var noon = new TimeSpan(12, 0, 0);
            var from = new DateTime(2008, 04, 25, 10, 23, 0);
            var to = new DateTime(2008, 04, 28, 9, 02, 0);

            var congestion = new Congestion(
                new ChargePeriod(am, pm, noon).CongestionPeriod(),
                new DateAndTime(from, to));
            var timeSpent = congestion.TimeSpent();
            
            Assert.AreEqual(timeSpent, (28, 162));
        }

        [TestMethod]
        public void Test_CountinuousDays()
        {
            var am = new TimeSpan(07, 0, 0);
            var pm = new TimeSpan(19, 0, 0);
            var noon = new TimeSpan(12, 0, 0);
            var from = new DateTime(2008, 04, 25, 10, 23, 0);
            var to = new DateTime(2008, 04, 28, 9, 02, 0);

            var congestion = new Congestion(
                new ChargePeriod(am, pm, noon).CongestionPeriod(),
                new DateAndTime(from, to));

            var continuousDays = congestion.ContinuousDays();

            Assert.AreEqual(continuousDays, 0);
        }

        [TestMethod]
        public void Test_Output_First()
        {
            var am = new TimeSpan(07, 0, 0);
            var pm = new TimeSpan(19, 0, 0);
            var noon = new TimeSpan(12, 0, 0);
            var from = new DateTime(2008, 04, 25, 10, 23, 0);
            var to = new DateTime(2008, 04, 28, 9, 02, 0);
            string type = "Car";
            double amTariff = 2;
            double pmTariff = 2.5;

        var congestion = new Congestion(
                new ChargePeriod(am, pm, noon).CongestionPeriod(),
                new DateAndTime(from, to));

            var vehicle = new VehicleCharge(
                 new VehicleType(type, amTariff, pmTariff).Type().VehicleCharge());

            var timeSpentAM = congestion.TimeSpent().amTime;
            var timeSpentPM = congestion.TimeSpent().pmTime;
            var continousDays = congestion.ContinuousDays();

            var period = congestion.ChargePeriod();
            var dayPrice = vehicle.OneDayCharge(period.amTotal, period.pmTotal);

            var amCharge = vehicle.ChargeAM(timeSpentAM);
            var pmCharge = vehicle.ChargePM(timeSpentPM);
            var totalCharge = vehicle.TotalCharge(timeSpentAM, timeSpentPM, continousDays, dayPrice);

            var output = new CongestionDisplay().Output(timeSpentAM, timeSpentPM, amCharge, pmCharge, totalCharge);
            var expectedFirst = $"Charge for 0h 28m (AM rate): \u00A30.90" + "\n" +
                                $"Charge for 2h 42m (PM rate): \u00A36.70" + "\n" +
                                $"Total Charge: \u00A37.60";
           
            Assert.AreEqual(output, expectedFirst);
        }

        [TestMethod]
        public void Test_Output_Second()
        {
            var am = new TimeSpan(07, 0, 0);
            var pm = new TimeSpan(19, 0, 0);
            var noon = new TimeSpan(12, 0, 0);
            var from = new DateTime(2008, 04, 25, 10, 23, 0);
            var to = new DateTime(2008, 04, 28, 9, 02, 0);
            string type = "Motorbike";
            double amTariff = 1;
            double pmTariff = 1;

            var congestion = new Congestion(
                new ChargePeriod(am, pm, noon).CongestionPeriod(),
                new DateAndTime(from, to));

            var vehicle = new VehicleCharge(
                 new VehicleType(type, amTariff, pmTariff).Type().VehicleCharge());

            var timeSpentAM = congestion.TimeSpent().amTime;
            var timeSpentPM = congestion.TimeSpent().pmTime;
            var continousDays = congestion.ContinuousDays();

            var period = congestion.ChargePeriod();
            var dayPrice = vehicle.OneDayCharge(period.amTotal, period.pmTotal);

            var amCharge = vehicle.ChargeAM(timeSpentAM);
            var pmCharge = vehicle.ChargePM(timeSpentPM);
            var totalCharge = vehicle.TotalCharge(timeSpentAM, timeSpentPM, continousDays, dayPrice);

            var output = new CongestionDisplay().Output(timeSpentAM, timeSpentPM, amCharge, pmCharge, totalCharge);
           
            var expectedSecond = $"Charge for 0h 0m (AM rate): \u00A30.00" + "\n" +
                                $"Charge for 2h 0m (PM rate): \u00A32.00" + "\n" +
                                $"Total Charge: \u00A32.00";
           
            Assert.AreEqual(output, expectedSecond);
        }

        [TestMethod]
        public void Test_Output_Third()
        {
            var am = new TimeSpan(07, 0, 0);
            var pm = new TimeSpan(19, 0, 0);
            var noon = new TimeSpan(12, 0, 0);
            var from = new DateTime(2008, 04, 25, 10, 23, 0);
            var to = new DateTime(2008, 04, 28, 9, 02, 0);
            string type = "Van";
            double amTariff = 2;
            double pmTariff = 2.5;

            var congestion = new Congestion(
                new ChargePeriod(am, pm, noon).CongestionPeriod(),
                new DateAndTime(from, to));

            var vehicle = new VehicleCharge(
                 new VehicleType(type, amTariff, pmTariff).Type().VehicleCharge());

            var timeSpentAM = congestion.TimeSpent().amTime;
            var timeSpentPM = congestion.TimeSpent().pmTime;
            var continousDays = congestion.ContinuousDays();

            var period = congestion.ChargePeriod();
            var dayPrice = vehicle.OneDayCharge(period.amTotal, period.pmTotal);

            var amCharge = vehicle.ChargeAM(timeSpentAM);
            var pmCharge = vehicle.ChargePM(timeSpentPM);
            var totalCharge = vehicle.TotalCharge(timeSpentAM, timeSpentPM, continousDays, dayPrice);

            var output = new CongestionDisplay().Output(timeSpentAM, timeSpentPM, amCharge, pmCharge, totalCharge);

            var expectedThird = $"Charge for 3h 39m (AM rate): \u00A37.30" + "\n" +
                                $"Charge for 7h 0m (PM rate): \u00A317.50" + "\n" +
                                $"Total Charge: \u00A324.80";

            Assert.AreEqual(output, expectedThird);
        }
    }
}
