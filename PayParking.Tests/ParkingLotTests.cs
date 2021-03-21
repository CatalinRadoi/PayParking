using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PayParking.Tests
{
    [TestClass]
    public class ParkingLotTests
    {
        private ParkingLot parkingLot;

        //todo de acoperit (așa în mare) - exista nr (parcheaza acelasi nr), e full, nu exista nr , stau doar 1s etc // nu ma intereseaza teste de validitate date - aia e treaba Presentation, care- fi el

        [TestInitialize]
        public void Init()
        {
            this.parkingLot = new ParkingLot(10, 10, 5);
        }
        
        [TestMethod]
        public void ParkVehicle_1h_ExpectPay10()
        {
            var startDate = new DateTime(2020, 2, 29, 10, 0, 0);
            this.ParkVehicle("dero", startDate);

            var toPay = this.parkingLot.ToPay("dero", startDate.AddHours(1));
            Assert.AreEqual(10,toPay);
        }

        [TestMethod]
        public void ParkVehicle_2h_ExpectPay15()
        {
            var startDate = new DateTime(2020, 2, 29, 10, 0, 0);
            this.ParkVehicle("dero", startDate);

            var toPay = this.parkingLot.ToPay("dero", startDate.AddHours(2));
            Assert.AreEqual(15,toPay);
        }


        [TestMethod]
        [ExpectedException(typeof(OverflowException), "Payment allowed for endDate < startDate")]
        public void ParkVehicle_negativeTime_OverflowException()
        {
            var startDate = DateTime.Now;
            this.ParkVehicle("dero", startDate);


            this.parkingLot.ToPay("dero", startDate.AddHours(-1));
        }


        [TestMethod]
        public void ParkVehicle_1h1s_ExpectPay15()
        {
            var startDate = new DateTime(2020, 2, 29, 10, 0, 0);
            this.ParkVehicle("dero", startDate);

            var toPay = this.parkingLot.ToPay("dero", startDate.AddHours(1).AddSeconds(1));
            Assert.AreEqual(15,toPay);
        }
        
        [TestMethod]
        public void ParkVehicle_5h15m2s_ExpectPay35()
        {
            var startDate = new DateTime(2020, 2, 29, 10, 0, 0);
            this.ParkVehicle("dero", startDate);
            var endDate = startDate.AddHours(5).AddMinutes(12).AddSeconds(2);

            var toPay = this.parkingLot.ToPay("dero", endDate);
            Assert.AreEqual(35,toPay);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Parking was allowed even though the parking lot was full.")]
        public void ParkVehicle_ParkingFull_Exception()
        {
            for (int i = 0; i < 10; i++)
            {
                this.ParkVehicle(i.ToString(),DateTime.Now);
            }

            this.ParkVehicle("I insist", DateTime.Now); // InvalidOperationException ♥
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "FreeParkingSpot worked for a non-existing license number")]
        public void FreeParkingSlot_ParkingEmpty_Exception()
        {
         
            this.parkingLot.FreeParkingSpot("I don't exist. Nothing is real."); // NullReferenceException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I was able to park the same vehicle twice")]
        public void ParkVehicle_AlreadyParked_Exception()
        {
            var vehicle1 = new Vehicle {LicenseNumber = "dero"};
            var vehicle2 = new Vehicle {LicenseNumber = "dero"};

            this.parkingLot.ParkVehicle(vehicle1, DateTime.Now); // țiplet
            this.parkingLot.ParkVehicle(vehicle2, DateTime.Now); // Custom ArgumentException
        }


        private void ParkVehicle(string licenseNumber, DateTime startDate)
        {
            this.parkingLot.ParkVehicle(new Vehicle
            {
                LicenseNumber = licenseNumber
            }, startDate);
        }
    }
}