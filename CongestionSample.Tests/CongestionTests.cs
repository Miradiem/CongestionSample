using CongestionSample.App.App;
using CongestionSample.App.Vehicles;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CongestionSample.Tests.App
{
    [TestClass]
    public class CongestionTests
    {
        [TestMethod]
        public void ShouldShowDayPrice()
        {
            var congestion = CreateCongestion();
            var vehicle = CreateVehicle();

            var period = congestion.ChargePeriodTotal();
            vehicle.PriceForDay(period.amTotal, period.pmTotal).Should().Be(27.5);
        }

        [TestMethod]
        public void ShouldShowChargeAM()
        {
            var congestion = CreateCongestion();
            var continuousDays = congestion.ContinuousDays();
            var period = congestion.ChargePeriodTotal();
            var vehicle = CreateVehicle();

            var timeSpent = congestion.TimeCharged(continuousDays, period.amTotal, period.pmTotal);
            Math.Round(vehicle.PriceForAM(timeSpent.amTime), 1, MidpointRounding.ToZero).Should().Be(0.90);
        }

        [TestMethod]
        public void ShouldShowChargePM()
        {
            var congestion = CreateCongestion();
            var continuousDays = congestion.ContinuousDays();
            var period = congestion.ChargePeriodTotal();
            var vehicle = CreateVehicle();

            var timeSpent = congestion.TimeCharged(continuousDays, period.amTotal, period.pmTotal);
            Math.Round(vehicle.PriceForPM(timeSpent.pmTime), 1, MidpointRounding.ToZero).Should().Be(6.70);
        }

        [TestMethod]
        public void ShouldShowTotalCharge()
        {
            var congestion = CreateCongestion();
            var continuousDays = congestion.ContinuousDays();
            var period = congestion.ChargePeriodTotal();
            var timeSpent = congestion.TimeCharged(continuousDays, period.amTotal, period.pmTotal);

            var vehicle = CreateVehicle();
            var dayPrice = vehicle.PriceForDay(period.amTotal, period.pmTotal);

            Math.Round(vehicle.TotalCharge(timeSpent.amTime, timeSpent.pmTime, continuousDays, dayPrice),
                       1, MidpointRounding.ToZero).Should().Be(7.60);
        }

        [TestMethod]
        public void ShouldShowHowManyFullWeekDays()
        {
            var congestion = CreateCongestion();
            congestion.ContinuousDays().Should().Be(0);
        }

        [TestMethod]
        public void ShouldShowTimeSpent()
        {
            var congestion = CreateCongestion();
            var continuousDays = congestion.ContinuousDays();
            var period = congestion.ChargePeriodTotal();
            var timeSpent = congestion.TimeCharged(continuousDays, period.amTotal, period.pmTotal);

            timeSpent.amTime.Should().Be(28);
            timeSpent.pmTime.Should().Be(162);
        }

        private static Congestion CreateCongestion()
        {
            var am = new TimeSpan(07, 0, 0);
            var pm = new TimeSpan(19, 0, 0);
            var noon = new TimeSpan(12, 0, 0);

            var from = new DateTime(2008, 04, 24, 11, 32, 0);
            var to = new DateTime(2008, 04, 24, 14, 42, 0);

            return new Congestion(am, pm, noon, from, to);
        }

        private static Vehicle CreateVehicle()
        {
            var type = "Car";

            return new Vehicle(type);
        }

       
    }
}
