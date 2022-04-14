using CongestionSample.App.App;
using CongestionSample.App.Vehicles;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace CongestionSampleTests
{
    public class CongestionTestsXUnit
    {
        [Theory, MemberData(nameof(TimeSpentData))]
        public void ShouldShowTimeSpent(Congestion congestion, (double, double) expected)
        {
            congestion.TimeCharged(congestion.ContinuousDays(), congestion.ChargePeriod())
                      .Should().Be(expected);
        }

        public static IEnumerable<object[]> TimeSpentData =>
             new List<object[]> {
                new object[] { new Congestion(
                                   new TimeSpan(07, 0, 0),
                                   new TimeSpan(19, 0, 0),
                                   new TimeSpan(12, 0, 0),
                                   new DateTime(2008, 04, 24, 11, 32, 0),
                                   new DateTime(2008, 04, 24, 14, 42, 0)), ((double) 28, (double) 162) },
                new object[] { new Congestion(
                                   new TimeSpan(07, 0, 0),
                                   new TimeSpan(19, 0, 0),
                                   new TimeSpan(12, 0, 0),
                                   new DateTime(2008, 04, 24, 10, 00, 0),
                                   new DateTime(2008, 04, 24, 14, 00, 0)), ((double) 120, (double) 120) },
                new object[] { new Congestion(
                                   new TimeSpan(07, 0, 0),
                                   new TimeSpan(19, 0, 0),
                                   new TimeSpan(12, 0, 0),
                                   new DateTime(2008, 04, 24, 9, 00, 0),
                                   new DateTime(2008, 04, 24, 15, 00, 0)), ((double) 180, (double) 180) }
             };

        [Theory, MemberData(nameof(VehicleData))]
        public void ShouldShowDayPrice(Congestion congestion, Vehicle vehicle, double expected)
        {
            vehicle.OneDayPrice(congestion.ChargePeriod().amTotal,
                                congestion.ChargePeriod().pmTotal)
                   .Should().Be(expected);
        }

        public static IEnumerable<object[]> VehicleData =>
            new List<object[]> {
                new object[] {new Congestion(
                                   new TimeSpan(07, 0, 0),
                                   new TimeSpan(19, 0, 0),
                                   new TimeSpan(12, 0, 0),
                                   new DateTime(2008, 04, 24, 11, 00, 0),
                                   new DateTime(2008, 04, 24, 14, 00, 0)),
                              new Vehicle("Car"), 27.5
                },
                new object[] { new Congestion(
                                   new TimeSpan(07, 0, 0),
                                   new TimeSpan(19, 0, 0),
                                   new TimeSpan(12, 0, 0),
                                   new DateTime(2008, 04, 24, 11, 00, 0),
                                   new DateTime(2008, 04, 24, 14, 00, 0)),
                              new Vehicle("Motorbike"), 27.5
                }
            };

        [Theory]
        [MemberData(nameof(AMPriceData))]
        public void ShouldShowPriceAM(Congestion congestion, Vehicle vehicle, double expected)
        {
            var chargePeriod = congestion.ChargePeriod();
            var continuousDays = congestion.ContinuousDays();
            var timeSpent = congestion.TimeCharged(continuousDays, chargePeriod);

            Math.Round(vehicle.PriceForAM(timeSpent.amTime), 1, MidpointRounding.ToZero).Should().Be(expected);
        }

        public static IEnumerable<object[]> AMPriceData =>
           new List<object[]> {
                new object[] { new Congestion(
                                   new TimeSpan(07, 0, 0),
                                   new TimeSpan(19, 0, 0),
                                   new TimeSpan(12, 0, 0),
                                   new DateTime(2008, 04, 24, 11, 32, 0),
                                   new DateTime(2008, 04, 24, 14, 42, 0)),
                               new Vehicle("Car"), 0.90
                },
                new object[] { new Congestion(
                                   new TimeSpan(07, 0, 0),
                                   new TimeSpan(19, 0, 0),
                                   new TimeSpan(12, 0, 0),
                                   new DateTime(2008, 04, 24, 11, 00, 0),
                                   new DateTime(2008, 04, 24, 14, 00, 0)),
                               new Vehicle("Motorbike"), 1
                }
           };

        [Theory, MemberData(nameof(PMPriceData))]
        public void ShouldShowPricePM(Congestion congestion, Vehicle vehicle, double expected)
        {
            var chargePeriod = congestion.ChargePeriod();
            var continuousDays = congestion.ContinuousDays();
            var timeSpent = congestion.TimeCharged(continuousDays, chargePeriod);

            Math.Round(vehicle.PriceForPM(timeSpent.pmTime), 1, MidpointRounding.ToZero).Should().Be(expected);
        }

        public static IEnumerable<object[]> PMPriceData =>
           new List<object[]> {
                new object[] { new Congestion(
                                   new TimeSpan(07, 0, 0),
                                   new TimeSpan(19, 0, 0),
                                   new TimeSpan(12, 0, 0),
                                   new DateTime(2008, 04, 24, 11, 32, 0),
                                   new DateTime(2008, 04, 24, 14, 42, 0)),
                               new Vehicle("Car"), 6.70
                },
                new object[] { new Congestion(
                                   new TimeSpan(07, 0, 0),
                                   new TimeSpan(19, 0, 0),
                                   new TimeSpan(12, 0, 0),
                                   new DateTime(2008, 04, 24, 11, 00, 0),
                                   new DateTime(2008, 04, 24, 14, 00, 0)),
                               new Vehicle("Motorbike"), 2
                }
           };

        [Theory, MemberData(nameof(TotalPriceData))]
        public void ShouldShowTotalPrice(Congestion congestion, Vehicle vehicle, double expected)
        {
            var chargePeriod = congestion.ChargePeriod();
            var continuousDays = congestion.ContinuousDays();
            var timeSpent = congestion.TimeCharged(continuousDays, chargePeriod);
            var oneDayPrice = vehicle.OneDayPrice(chargePeriod.amTotal, chargePeriod.pmTotal);

            Math.Round(vehicle.TotalPrice(timeSpent.amTime, timeSpent.pmTime, continuousDays, oneDayPrice),
                       1, MidpointRounding.ToZero).Should().Be(expected);
        }

        public static IEnumerable<object[]> TotalPriceData =>
            new List<object[]> {
                new object[] { new Congestion(
                                   new TimeSpan(07, 0, 0),
                                   new TimeSpan(19, 0, 0),
                                   new TimeSpan(12, 0, 0),
                                   new DateTime(2008, 04, 24, 11, 32, 0),
                                   new DateTime(2008, 04, 24, 14, 42, 0)),
                               new Vehicle("Car"), 7.60
                },
                new object[] { new Congestion(
                                   new TimeSpan(07, 0, 0),
                                   new TimeSpan(19, 0, 0),
                                   new TimeSpan(12, 0, 0),
                                   new DateTime(2008, 04, 24, 11, 00, 0),
                                   new DateTime(2008, 04, 24, 14, 00, 0)),
                               new Vehicle("Motorbike"), 3
                }
            };
    }
}
