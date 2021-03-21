using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;

namespace PayParking
{
    public class ParkingLot
    {
        public ParkingLot(int totalSpots, double firstHourRate, double hourlyRate)
        {
            this.ParkingSpots = new List<ParkingSpot>();
            for (var i = 1; i <= totalSpots; i++)
            {
                this.ParkingSpots.Add(new ParkingSpot
                {
                    Number = i.ToString()
                });
            }


            this.FirstHourRate = firstHourRate;
            this.HourlyRate = hourlyRate;
        }

        public double FirstHourRate { get; }
        public double HourlyRate { get; }

        public List<ParkingSpot> ParkingSpots { get; set; }

        public int CountAvailableSpots()
        {
            return this.ParkingSpots.Count(ps => ps.Vehicle == null);
        }

        public void FreeParkingSpot(string licenseNumber)
        {
            var spot = this.ParkingSpots.First(ps =>
                ps.Vehicle.LicenseNumber.Equals(licenseNumber, StringComparison.InvariantCultureIgnoreCase));

            spot.Vehicle = null;
        }

        public bool VehicleExists(string licenseNumber)
        {
            return this.ParkingSpots.Any(ps => ps.Vehicle != null &&
                                               ps.Vehicle.LicenseNumber.Equals(licenseNumber,
                                                   StringComparison.InvariantCultureIgnoreCase));

        }

        public string GetStatus()
        {
            var sb = new StringBuilder();
            sb.Append("------------------------------------------------------------------------------");
            sb.Append(Environment.NewLine);
            foreach (var parkingSpot in this.ParkingSpots)
            {
                sb.Append($"Parking Spot {parkingSpot.Number}:");
                sb.Append($"\t{(parkingSpot.Number == "10" ? string.Empty : "\t")}");
                sb.Append(parkingSpot.IsFree ? "Empty" : parkingSpot.Vehicle.LicenseNumber.ToUpper());
                sb.Append("\t");
                if (!parkingSpot.IsFree)
                {
                    sb.Append(parkingSpot.StartTime.ToString("dd-MM-yyyy HH:mm:ss"));

                    sb.Append("\t");
                    sb.Append(this.ToPay(parkingSpot, DateTime.Now) + " lei");
                }

                sb.Append(Environment.NewLine);
            }

            sb.Append("------------------------------------------------------------------------------");
            sb.Append(Environment.NewLine);
            return sb.ToString();
        }

        public bool IsFull()
        {
            return this.CountAvailableSpots() == 0;
        }

        public bool IsEmpty()
        {
            return this.CountAvailableSpots() == this.ParkingSpots.Count;
        }


        public ParkingSpot GetParkingSpotByLicenseNumber(string licenseNumber)
        {
            return this.ParkingSpots.First(ps => ps.Vehicle != null && ps.Vehicle.LicenseNumber.Equals(licenseNumber,
                                                     StringComparison.InvariantCultureIgnoreCase));
        }

        public string WelcomeMessage()
        {
            return $"Welcome to NU-STIU Parking | {this.FirstHourRate}lei first hour, then {this.HourlyRate}lei hourly.";

        }

        public void ParkVehicle(Vehicle vehicle, DateTime startTime)
        {
            if (this.ParkingSpots.Any(ps => ps.Vehicle != null && ps.Vehicle.LicenseNumber.Equals(vehicle.LicenseNumber,
                                                StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new ArgumentException("Vehicle with this license number already parked.");
            }

            var spot = this.ParkingSpots.First(ps => ps.Vehicle == null);
            spot.Vehicle = vehicle;
            spot.StartTime = startTime;
        }


        public double ToPay(ParkingSpot parkingSpot, DateTime endTime)
        {
            var totalHours = (int) Math.Ceiling((endTime- parkingSpot.StartTime).TotalHours);

            return this.FirstHourRate + (totalHours - 1) * this.HourlyRate;
        }

        public double ToPay(string licenseNumber, DateTime endTime)
        {
            var spot = this.ParkingSpots.First(ps => ps.Vehicle.LicenseNumber.Equals(licenseNumber, StringComparison.InvariantCultureIgnoreCase));
            if (endTime < spot.StartTime)
            {
                throw new OverflowException("EndDate less than StartDate");
            }
            return this.ToPay(spot, endTime);
        }

    
    }
}