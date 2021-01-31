using System;
using System.Collections.Generic;
using System.Linq;

namespace PayParking
{
   public class ParkingLot
   {
       //public int TotalSpots { get; }
       public double FirstHourRate { get; }
       public double HourlyRate { get;  }

        public List<ParkingSpot> ParkingSpots { get; set; }

       public ParkingLot(int totalSpots, double firstHourRate, double hourlyRate)
       {
           //this.TotalSpots = totalSpots;
           // ToDo create # Parkign SLots _ momentan generez eu numere / locurile de parcare


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


        }

        public void FreeParkingSpot(string licenseNumber)
        {
            var spot = this.ParkingSpots.First(ps =>
                ps.Vehicle.LicenseNumber.Equals(licenseNumber, StringComparison.InvariantCultureIgnoreCase));

            spot.Vehicle = null;
        }

    }
}
