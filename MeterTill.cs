using ParkingMeter_Refactored.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingMeter_Refactored
{
    public class MeterTill : IMeterTill
    {
        private int tillCapacity;
        private int amountInTill;
        public MeterTill(int capacity)
        {
            tillCapacity = capacity;
            amountInTill = 0;
        }

        public bool AddQuarter()
        {
            bool addedToTill = false;

            if(!IsTillFull())
            {
                amountInTill++;
                addedToTill = true;
            }
            return addedToTill;
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

        public int GetTillCapacity()
        {
            return tillCapacity;
        }
    }
}
