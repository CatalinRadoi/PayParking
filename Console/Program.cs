using System;
using System.Runtime.CompilerServices;
using PayParking;

namespace Console
{
    internal static class Program
    {

        private static ParkingLot parkingLot;

        static void Main(string[] args)
        {
            parkingLot = new ParkingLot(10, 10, 5);



            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }
        private static bool MainMenu()
        {
            System.Console.Clear();
            System.Console.WriteLine("Choose an option:");
            System.Console.WriteLine("1) Park Vehicle");
            System.Console.WriteLine("2) Free Parking Spot");
            System.Console.WriteLine("3) Exit");
            System.Console.Write("\r\nSelect an option: ");
 
            switch (System.Console.ReadLine())
            {
                case "1":
                    ParkVehicle();
                    return true;
                case "2":
                    FreeParkingSlot();
                    return true;
                case "3":
                    return false;
                default:
                    return true;
            }
        }
 
        private static string GetLicenseNumber()
        {
            System.Console.Write("License Number: ");
            return System.Console.ReadLine();
        }
 
        private static void ParkVehicle()
        {
            System.Console.Clear();
            System.Console.WriteLine("Parking...cutare");
 

            var vehicle = new Vehicle {LicenseNumber = GetLicenseNumber()};

            parkingLot.ParkVehicle(vehicle);

            //DisplayResult(String.Concat(charArray));
        }
 
        private static void FreeParkingSlot()
        {
            System.Console.Clear();
            System.Console.WriteLine("FreeParkingSlot");
 
            DisplayResult(GetLicenseNumber().Replace(" ", ""));
        }
 
        private static void DisplayResult(string message)
        {
            System.Console.WriteLine($"\r\nYour modified string is: {message}");
            System.Console.Write("\r\nPress Enter to return to Main Menu");
            System.Console.ReadLine();
        }
    }
}
