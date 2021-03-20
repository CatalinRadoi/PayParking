using System;
using System.Collections.Generic;
using System.Linq;

namespace PayParking
{
   public class ParkingLot
   {
       public double FirstHourRate { get; }
       public double HourlyRate { get;  }

        public List<ParkingSpot> ParkingSpots { get; set; }

       public ParkingLot(int totalSpots, double firstHourRate, double hourlyRate)
       {
           for (int i = 1; i <= totalSpots; i++)
           {
               this.ParkingSpots.Add(new ParkingSpot
               {
                   IsFree = true,
                   Number = i.ToString()
               });
           }


           this.FirstHourRate = firstHourRate;
           this.HourlyRate = hourlyRate;
       }


       public bool IsFull()
       {
           return this.CountAvailableSpots() == 0;
       }

        public int CountAvailableSpots()
        {
            return this.ParkingSpots.Count(ps => ps.Vehicle == null);
        }

        public void ParkVehicle(Vehicle vehicle)
        {
            var spot = this.ParkingSpots.First(ps => ps.Vehicle == null);
            spot.Vehicle = vehicle;
            spot.StartTime = DateTime.Now;
        }

        public void FreeParkingSpot(string licenseNumber)
        {
            var spot = this.ParkingSpots.First(ps =>
                ps.Vehicle.LicenseNumber.Equals(licenseNumber, StringComparison.InvariantCultureIgnoreCase));

            spot.Vehicle = null;
            spot.EndTime = DateTime.Now;
        }

    }
}
