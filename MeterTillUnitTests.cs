using NUnit.Framework;
using ParkingMeter_Refactored.Interfaces;

namespace ParkingMeter_Refactored.UnitTests
{
    [TestFixture]
    public class MeterTillUnitTests
    {
        [Test]
        public void MeterTill_GetTillValueWhenEmpty_ReturnsZero()
        {
            IMeterTill subject = new MeterTill(20);

            int value = subject.GetTillValue();

            Assert.That(value, Is.Zero);
        }

        [Test]
        public void MeterTill_GetTillValueWhenOneQuarter_ReturnsOne()
        {
            IMeterTill subject = new MeterTill(20);

            subject.AddQuarter();
            int value = subject.GetTillValue();

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
        public void MeterTill_GetTillValue_MatchesTestCase(int addCount, int tillValue)
        {
            IMeterTill subject = new MeterTill(20);

            for(int x = 0; x < addCount; x++)
            {
                subject.AddQuarter();
            }

            int value = subject.GetTillValue();

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
        public void MeterTill_NewTillIsNotFull_ReturnsFalse(int addCount, bool isFull)
        {
            IMeterTill subject = new MeterTill(20);

            for(int x = 0; x < addCount; x++)
            {
                subject.AddQuarter();
            }

            bool isEmpty = subject.IsTillFull();

            Assert.That(isEmpty, Is.EqualTo(isFull));
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(21)]
        public void MeterTill_EmptyTill_TillValueIsZero(int addCount)
        {
            IMeterTill subject = new MeterTill(20);

            for(int x = 0; x < addCount; x++)
            {
                subject.AddQuarter();
            }
            subject.EmptyTill();

            int tillValue = subject.GetTillValue();

            Assert.That(tillValue, Is.Zero);
        }

        [Test]
        public void MeterTill_TillCapacity_ReturnsTwenty()
        {
            IMeterTill subject = new MeterTill(20);

            Assert.That(subject.GetTillCapacity(), Is.EqualTo(20));
        }
    }
}
