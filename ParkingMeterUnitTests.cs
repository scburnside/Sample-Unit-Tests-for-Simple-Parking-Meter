using NUnit.Framework;
using ParkingMeter_Simple;

namespace ParkingMeter_Simple.UnitTests
{
    [TestFixture]
    public class ParkingMeterUnitTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void ParkingMeter_CanInitialize_ReturnsValidObject()
        {
            ParkingMeter parkingMeter = new ParkingMeter();

            Assert.That(parkingMeter, Is.Not.Null);
        }

        [Test]
        public void ParkingMeter_CanAddQuarterWhenTillNotFull_ReturnsTrue()
        {
            ParkingMeter parkingMeter = new ParkingMeter();

            bool isAdded = parkingMeter.AddQuarter();

            Assert.That(isAdded, Is.True);
        }

        [Test]
        public void ParkingMeter_CanNotAddQuarterWhenTillIsFull_ReturnsFalse()
        {
            ParkingMeter parkingMeter = new ParkingMeter();

            //add unitl the till is full, these should all succeed
            bool goodAdd = true;
            for(int addCount = 0; addCount < parkingMeter.GetTillCapacity(); addCount++)
            {
                goodAdd = goodAdd & parkingMeter.AddQuarter();
            }

            //the till should be full, this should now overflow the till and return false
            bool badAdd = parkingMeter.AddQuarter();

            Assert.Multiple(() =>
           {
               Assert.That(goodAdd, Is.True);
               Assert.That(badAdd, Is.False);
           });
        }

        [Test]
        public void ParkingMeter_GetRemainingTimeWhenQuarterIsAdded_IsGreaterThanZero()
        {
            ParkingMeter parkingMeter = new ParkingMeter();

            parkingMeter.AddQuarter();
            double remainingTime = parkingMeter.GetRemainingTime();

            Assert.That(remainingTime, Is.GreaterThan(0));
        }

        [Test]
        public void ParkingMeter_GetRemainingTimeWhenQuarterIsNotAdded_IsZero()
        {
            ParkingMeter parkingMeter = new ParkingMeter();

            double remainingTime = parkingMeter.GetRemainingTime();

            Assert.That(remainingTime, Is.Zero);
        }

        [Test]
        public void ParkingMeter_GetRemainingTimeWhenTimeExpires_IsZero()
        {
            ParkingMeter parkingMeter = new ParkingMeter();

            parkingMeter.AddQuarter();

            //THIS TEST HAS TO WAIT 15 MINUTES?  IMPOSSIBLE TO RUN
            double remainingTime = parkingMeter.GetRemainingTime();

            Assert.That(remainingTime, Is.Zero);
        }

        [Test]
        public void ParkingMeter_IsExpiredTimeWhenQuarterIsAdded_ReturnsFalse()
        {
            ParkingMeter parkingMeter = new ParkingMeter();

            parkingMeter.AddQuarter();
            bool isExpired = parkingMeter.IsExpired();

            Assert.That(isExpired, Is.False);
        }

        [Test]
        public void ParkingMeter_IsExpiredTimeWhenQuarterIsNotAdded_ReturnsTrue()
        {
            ParkingMeter parkingMeter = new ParkingMeter();

            bool isExpired = parkingMeter.IsExpired();

            Assert.That(isExpired, Is.True);
        }

        [Test]
        public void ParkingMeter_IsExpiredTimeWhenTimeExpires_ReturnsTrue()
        {
            ParkingMeter parkingMeter = new ParkingMeter();

            parkingMeter.AddQuarter();

            //THIS TEST HAS TO WAIT 15 MINUTES?  IMPOSSIBLE TO RUN
            bool isExpired = parkingMeter.IsExpired();

            Assert.That(isExpired, Is.True);
        }

        [Test]
        public void ParkingMeter_GetTillValueWhenEmpty_ReturnsZero()
        {
            ParkingMeter parkingMeter = new ParkingMeter();

            int value = parkingMeter.GetTillValue();

            Assert.That(value, Is.Zero);
        }

        [Test]
        public void ParkingMeter_GetTillValueWhenOneQuarter_ReturnsOne()
        {
            ParkingMeter parkingMeter = new ParkingMeter();

            parkingMeter.AddQuarter();
            int value = parkingMeter.GetTillValue();

            Assert.That(value, Is.EqualTo(1));
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(10, 10)]
        [TestCase(20, 20)]
        [TestCase(21, 20)]
        [TestCase(100, 20)]
        public void ParkingMeter_GetTillValue_MatchesTestCase(int addCount, int tillValue)
        {
            ParkingMeter parkingMeter = new ParkingMeter();

            for(int x = 0; x < addCount; x++)
            {
                parkingMeter.AddQuarter();
            }

            int value = parkingMeter.GetTillValue();

            Assert.That(value, Is.EqualTo(tillValue));
        }

        [Test]
        [TestCase(0, false)]
        [TestCase(1, false)]
        [TestCase(2, false)]
        [TestCase(10, false)]
        [TestCase(20, true)]
        [TestCase(21, true)]
        [TestCase(100, true)]
        public void ParkingMeter_NewTillIsNotFull_ReturnsFalse(int addCount, bool isFull)
        {
            ParkingMeter parkingMeter = new ParkingMeter();

            for(int x = 0; x < addCount; x++)
            {
                parkingMeter.AddQuarter();
            }

            bool isEmpty = parkingMeter.IsTillFull();

            Assert.That(isEmpty, Is.EqualTo(isFull));
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(21)]
        public void ParkingMeter_EmptyTill_TillValueIsZero(int addCount)
        {
            ParkingMeter parkingMeter = new ParkingMeter();

            for(int x = 0; x < addCount; x++)
            {
                parkingMeter.AddQuarter();
            }
            parkingMeter.EmptyTill();
            int tillValue = parkingMeter.GetTillValue();

            Assert.That(tillValue, Is.Zero);
        }

        [Test]
        public void ParkingMeter_TillCapacity_ReturnsTwenty()
        {
            ParkingMeter parkingMeter = new ParkingMeter();

            Assert.That(parkingMeter.GetTillCapacity(), Is.EqualTo(20));
        }
    }
}