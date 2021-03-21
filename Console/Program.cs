using System;
using PayParking;

namespace Console
{
    internal static class Program
    {
        private static ParkingLot parkingLot;

        private static void FreeParkingSlot()
        {
            System.Console.Clear();
            System.Console.WriteLine("Free Parking Slot");

            var licenseNumber = GetLicenseNumber();

            System.Console.WriteLine();
            if (parkingLot.VehicleExists(licenseNumber))
            {
                var endDate = DateTime.Now;
                var toPay = parkingLot.ToPay(licenseNumber, endDate);
                var parkingSpot = parkingLot.GetParkingSpotByLicenseNumber(licenseNumber);

                System.Console.WriteLine("-----------------------------------------------------------------");
                System.Console.WriteLine("Start Date: " + parkingSpot.StartTime);
                System.Console.WriteLine("End Date: " + endDate);
                System.Console.WriteLine($"Total payment: {toPay} lei");
                System.Console.WriteLine("-----------------------------------------------------------------");
                System.Console.WriteLine("Press ENTER to scan Credit Card...");
                System.Console.ReadLine();
                parkingLot.FreeParkingSpot(licenseNumber);
            }
            else
            {
                System.Console.WriteLine("No vehicle found. You need to work on those typing skills");
                System.Console.WriteLine("Press ENTER (the largest key) to return to menu.............");
                System.Console.ReadLine();
            }
        }

        private static string GetLicenseNumber()
        {
            System.Console.Write("License Number: ");
            return System.Console.ReadLine();
        }

        private static void Main()
        {
            parkingLot = new ParkingLot(10, 10, 5);

            var showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        private static bool MainMenu()
        {
            System.Console.Clear();
            PrintWelcome();
            PrintStatus();
            System.Console.WriteLine();
            System.Console.WriteLine("Choose an option:");

            if (!parkingLot.IsFull())
            {
                System.Console.WriteLine("1) Park Vehicle");
            }

            if (!parkingLot.IsEmpty())
            {
                System.Console.WriteLine("2) Free Parking Spot");
            }

            System.Console.WriteLine("0) Exit");
            System.Console.Write("\r\nSelect an option: ");

            switch (System.Console.ReadLine())
            {
                case "1":
                    if (!parkingLot.IsFull())
                    {
                        ParkVehicle();
                    }

                    return true;
                case "2":
                    if (!parkingLot.IsEmpty())
                    {
                        FreeParkingSlot();
                    }

                    return true;
                case "0":
                    System.Console.Clear();
                    System.Console.WriteLine("You can go outside and play now!");
                    System.Console.Beep(); // TODO TEST pe un laptop cu sunet să nu facă urât, pe ăsta nu am drivere
                    System.Threading.Thread.Sleep(3000);
                    return false;
                default:
                    return true;
            }
        }

        private static void ParkVehicle()
        {
            System.Console.Clear();
            System.Console.WriteLine("Park Vehicle");
            var licenseNumber = GetLicenseNumber();
            System.Console.WriteLine();

            if (!parkingLot.VehicleExists(licenseNumber))
            {
                var vehicle = new Vehicle {LicenseNumber = licenseNumber};

                parkingLot.ParkVehicle(vehicle, DateTime.Now);
            }
            else
            {
                   System.Console.WriteLine("Vehicle already parked! Did you have your coffee today?"); 
                   System.Console.ReadLine();
            }
        }

        private static void PrintWelcome()
        {
            System.Console.WriteLine(parkingLot.WelcomeMessage());
        }
        private static void PrintStatus()
        {
            System.Console.Write(parkingLot.GetStatus());
        }
    }
}