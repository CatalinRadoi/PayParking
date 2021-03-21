using System;

namespace PayParking
{
    public class ParkingSpot
    {
        public bool IsFree => this.Vehicle == null;

        public string Number { get; set; }

        public DateTime StartTime { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}