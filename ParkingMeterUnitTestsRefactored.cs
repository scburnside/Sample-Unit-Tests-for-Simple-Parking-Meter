using Moq;
using NUnit.Framework;
using ParkingMeter_Refactored;
using ParkingMeter_Refactored.Interfaces;
using System;

namespace ParkingMeter_Refactored.UnitTests
{
    [TestFixture]
    public class ParkingMeter_UnitTests
    {
        private IDateTimeProvider dateTimeProvider;
        private IMeterTill meterTill;

        [SetUp]
        public void Setup()
        {
            dateTimeProvider = new DateTimeProvider();
            meterTill = new MeterTill(20);
        }

        private int dateTimeCalls = 0;
        DateTime mockNow;
        Mock<IDateTimeProvider> createFakeDateTime()
        {
            Mock<IDateTimeProvider> mockProvider = new Mock<IDateTimeProvider>(); //Create mock provider 

            mockProvider.Setup(m => m.Now).Callback(() => //Set up for when now is called. 
           {
               DateTime now = DateTime.Now;
               mockNow = now.AddMinutes(dateTimeCalls * 15); //Adds 15 minutes to now
               dateTimeCalls++;
           }).Returns(mockNow);

            return mockProvider;
        }

        Mock<IMeterTill> CreateFakeEmptyMeterTill()
        {
            Mock<IMeterTill> mockTill = new Mock<IMeterTill>();
            mockTill.Setup(m => m.AddQuarter()).Returns(true);
            return mockTill;
        }

        Mock<IMeterTill> CreateFakeFullMeterTill()
        {
            Mock<IMeterTill> mockTill = new Mock<IMeterTill>();
            mockTill.Setup(m => m.AddQuarter()).Returns(false);
            return mockTill;
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void ParkingMeter_CanInitialize_ReturnsValidObject()
        {
            ParkingMeter parkingMeter = new ParkingMeter(dateTimeProvider, meterTill);

            Assert.That(parkingMeter, Is.Not.Null);
        }

        [Test]
        public void ParkingMeter_CanAddQuarterWhenTillNotFull_ReturnsTrue()
        {
            ParkingMeter parkingMeter = new ParkingMeter(dateTimeProvider, CreateFakeEmptyMeterTill().Object);

            bool isAdded = parkingMeter.AddQuarter();

            Assert.That(isAdded, Is.True);
        }

        [Test]
        public void ParkingMeter_CanNotAddQuarterWhenTillIsFull_ReturnsFalse()
        {
            ParkingMeter parkingMeter = new ParkingMeter(dateTimeProvider, CreateFakeFullMeterTill().Object);

            bool badAdd = parkingMeter.AddQuarter();

            Assert.That(badAdd, Is.False);
        }

        [Test]
        public void ParkingMeter_GetRemainingTimeWhenQuarterIsAdded_IsGreaterThanZero()
        {
            ParkingMeter parkingMeter = new ParkingMeter(dateTimeProvider, meterTill);

            parkingMeter.AddQuarter();
            double remainingTime = parkingMeter.GetRemainingTime();

            Assert.That(remainingTime, Is.GreaterThan(0));
        }

        [Test]
        public void ParkingMeter_GetRemainingTimeWhenQuarterIsNotAdded_IsZero()
        {
            ParkingMeter parkingMeter = new ParkingMeter(dateTimeProvider, meterTill);

            double remainingTime = parkingMeter.GetRemainingTime();

            Assert.That(remainingTime, Is.Zero);
        }

        [Test]
        public void ParkingMeter_GetRemainingTimeWhenTimeExpires_IsZero()
        {
            ParkingMeter parkingMeter = new ParkingMeter(createFakeDateTime().Object, meterTill);

            parkingMeter.AddQuarter();

            //THIS TEST HAS TO WAIT 15 MINUTES?  IMPOSSIBLE TO RUN
            double remainingTime = parkingMeter.GetRemainingTime();

            Assert.That(remainingTime, Is.Zero);
        }

        [Test]
        public void ParkingMeter_IsExpiredTimeWhenQuarterIsAdded_ReturnsFalse()
        {
            ParkingMeter parkingMeter = new ParkingMeter(dateTimeProvider, meterTill);

            parkingMeter.AddQuarter();
            bool isExpired = parkingMeter.IsExpired();

            Assert.That(isExpired, Is.False);
        }

        [Test]
        public void ParkingMeter_IsExpiredTimeWhenQuarterIsNotAdded_ReturnsTrue()
        {
            ParkingMeter parkingMeter = new ParkingMeter(dateTimeProvider, meterTill);

            bool isExpired = parkingMeter.IsExpired();

            Assert.That(isExpired, Is.True);
        }

        [Test]
        public void ParkingMeter_IsExpiredTimeWhenTimeExpires_ReturnsTrue()
        {
            ParkingMeter parkingMeter = new ParkingMeter(createFakeDateTime().Object, meterTill);

            parkingMeter.AddQuarter();

            //THIS TEST HAS TO WAIT 15 MINUTES?  IMPOSSIBLE TO RUN
            bool isExpired = parkingMeter.IsExpired();

            Assert.That(isExpired, Is.True);
        }
    }
}