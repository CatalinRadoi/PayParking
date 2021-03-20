using System;

namespace PayParking
{
    public class ParkingSpot
    {
        public string Number { get; set; }

        public bool IsFree { get; set; }

        public Vehicle Vehicle { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

    }
}
