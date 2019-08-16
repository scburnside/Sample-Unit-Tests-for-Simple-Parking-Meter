using ParkingMeter_Refactored.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingMeter_Refactored
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}
