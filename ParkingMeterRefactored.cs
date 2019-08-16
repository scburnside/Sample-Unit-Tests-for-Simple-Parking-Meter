using ParkingMeter_Refactored.Interfaces;
using System;

namespace ParkingMeter_Refactored
{
    public class ParkingMeter
    {
        private DateTime expiringTime;

        private IDateTimeProvider DateTimeProvider;
        private IMeterTill MeterTill;

        public ParkingMeter(IDateTimeProvider dateTimeProvider, IMeterTill meterTill)
        {
            DateTimeProvider = dateTimeProvider;
            MeterTill = meterTill;

            expiringTime = DateTime.MinValue;
        }

        public bool AddQuarter()
        {
            bool addedToTill = false;

            if(MeterTill.AddQuarter())
            {
                if(IsExpired())
                {
                    expiringTime = DateTimeProvider.Now.AddMinutes(15);
                }
                else
                {
                    expiringTime = expiringTime.AddMinutes(15);  //This is a bug, not setting expiringTime = expringTime.AddMinutes(15)
                }

                addedToTill = true;
            }
            return addedToTill;
        }

        public double GetRemainingTime()
        {
            double minutesRemaining = 0;
            if(!IsExpired())
            {
                TimeSpan remainingTime = expiringTime.Subtract(DateTimeProvider.Now);
                minutesRemaining = remainingTime.TotalMinutes;  //Change TotalMinutes to Minutes for a bug
            }

            return minutesRemaining;
        }

        public bool IsExpired()
        {
            return DateTimeProvider.Now >= expiringTime;  //Change to >= to get an edge condition bug
        }


    }
}
