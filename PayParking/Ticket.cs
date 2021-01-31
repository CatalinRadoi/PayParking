using System;

namespace PayParking
{
   public class Ticket
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public Vehicle Vehicle { get; set; }
        
        public ParkingSpot ParkingSpot { get; set; }
    }
}
