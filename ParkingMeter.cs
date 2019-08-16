using System;

namespace ParkingMeter_Simple
{
    
    /// A simple Parking Meter class
    
    /// A user can add money to the meter which will add more time
    /// The user and attendant can see how much remaining time is left
    /// The user and attendant can see if the meter is expired
    /// The attendant can see if the meters till is full
    /// The attendant can see the capacity of the till and how much is in it
    /// The attendant can empty the till
    public class ParkingMeter
    {
        private DateTime expiringTime;
        private int tillCapacity;
        private int amountInTill;

        public ParkingMeter()
        {
            expiringTime = DateTime.MinValue;
            tillCapacity = 20;
            amountInTill = 0;
        }

        public bool AddQuarter()
        {
            bool addedToTill = false;

            if(!IsTillFull())
            {
                if(IsExpired())
                {
                    expiringTime = DateTime.Now.AddMinutes(15);
                }
                else
                {
                    expiringTime.AddMinutes(15);
                }

                amountInTill++;
                addedToTill = true;
            }
            return addedToTill;
        }

        public double GetRemainingTime()
        {
            double minutesRemaining = 0;
            if(!IsExpired())
            {
                TimeSpan remainingTime = expiringTime.Subtract(DateTime.Now);
                minutesRemaining = remainingTime.TotalMinutes;  //Change TotalMinutes to Minutes for a bug
            }

            return minutesRemaining;
        }

        public bool IsExpired()
        {
            return DateTime.Now > expiringTime;  //Change to >= to get an edge condition bug
        }

        public int GetTillValue()
        {
            return amountInTill;
        }

        public bool IsTillFull()
        {
            return amountInTill == tillCapacity;
        }

        public void EmptyTill()
        {
            amountInTill = 0;
        }

        public int GetTillCapacity() //How many coins can fit
        {
            return tillCapacity;
        }
    }
}
