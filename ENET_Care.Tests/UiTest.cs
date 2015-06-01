using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ENET_Care.Controllers;
using System.Web;
using System.Security.Principal;
using System.Security.Claims;
using System.Web.Mvc;

namespace ENET_Care.Tests
{
    [TestClass]
    public class UiTest
    {
        /***********************
         * Register Package Test
         * *********************/
        [TestMethod]
        public void Ui_SendPackage_BadBarcode()
        {
            PackageController controller = SetupUser();
            controller.Send("notanumber", 1);
            Assert.AreEqual("Please enter a valid barcode", controller.ViewBag.Error);
        }

        /// <summary>
        /// Attempts to register a package that is expired.
        /// The action should not crash and should send the user back
        /// to the package registration page with the error message
        /// </summary>
        [TestMethod]
        public void Ui_RegisterPackage_PastExpiryDate()
        {
            PackageController controller = SetupUser();
            ActionResult ar = controller.Register(1, "01/01/2000");
            Assert.AreEqual("The package is expired", controller.ViewBag.Error); 
            Assert.ReferenceEquals(controller.Register(), ar); //The user is sent back to the registration page
        }

       
        /***********************
         * Receive Package Test
         * *********************/
        /// <summary>
        /// Attempts to put words as barcode
        /// </summary>
        [TestMethod]
        public void Ui_ReceivePackage_StringInput()
        {
            PackageStatusController controller = SetupPackageStatus();
            ActionResult ar = controller.Receive("String","19");
            Assert.AreEqual("Please enter a valid barcode", controller.ViewBag.Error);
            Assert.ReferenceEquals(controller.Receive(), ar); //The user is sent back to the receive page
        }
        /// <summary>
        /// Attempts to put unregistered package as barcode
        /// </summary>
        [TestMethod]
        public void Ui_ReceivePackage_UnregisteredPackageInput()
        {
            PackageStatusController controller = SetupPackageStatus();
            ActionResult ar = controller.Receive("90", "19");
            Assert.AreEqual("The barcode you have entered doesn't exist/has not been registered", controller.ViewBag.Error);
            Assert.ReferenceEquals(controller.Receive(), ar); //The user is sent back to the receive page
        }
        /// <summary>
        /// Attempts to put long integer as barcode
        /// </summary>
        [TestMethod]
        public void Ui_ReceivePackage_LongIntegerInput()
        {
            PackageStatusController controller = SetupPackageStatus();
            ActionResult ar = controller.Receive("124426427224526245266", "19");
            Assert.AreEqual("Please enter a valid barcode", controller.ViewBag.Error);
            Assert.ReferenceEquals(controller.Receive(), ar); //The user is sent back to the receive page
        }
        /***********************
         * Discard Package Test
         * *********************/
        /// <summary>
        /// Attempts to put words as barcode
        /// </summary>
        [TestMethod]
        public void Ui_DiscardPackage_StringInput()
        {
            PackageStatusController controller = SetupPackageStatus();
            ActionResult ar = controller.Discard("String", "19");
            Assert.AreEqual("Please enter a valid barcode", controller.ViewBag.Error);
            Assert.ReferenceEquals(controller.Discard(), ar); //The user is sent back to the discard page
        }
        /// <summary>
        /// Attempts to put unregistered package as barcode
        /// </summary>
        [TestMethod]
        public void Ui_DiscardPackage_UnregisteredPackageInput()
        {
            PackageStatusController controller = SetupPackageStatus();
            ActionResult ar = controller.Discard("90", "19");
            Assert.AreEqual("The barcode you have entered doesn't exist/has not been registered", controller.ViewBag.Error);
            Assert.ReferenceEquals(controller.Discard(), ar); //The user is sent back to the discard page
        }
        /// <summary>
        /// Attempts to put long integer as barcode
        /// </summary>
        [TestMethod]
        public void Ui_DiscardPackage_LongIntegerInput()
        {
            PackageStatusController controller = SetupPackageStatus();
            ActionResult ar = controller.Discard("124426427224526245266", "19");
            Assert.AreEqual("Please enter a valid barcode", controller.ViewBag.Error);
            Assert.ReferenceEquals(controller.Discard(), ar); //The user is sent back to the discard page
        }
        /***********************
         * Distribute Package Test
         * *********************/
        /// <summary>
        /// Attempts to put words as barcode
        /// </summary>
        [TestMethod]
        public void Ui_DistributePackage_StringInput()
        {
            PackageStatusController controller = SetupPackageStatus();
            ActionResult ar = controller.Distribute("String", "19");
            Assert.AreEqual("Please enter a valid barcode", controller.ViewBag.Error);
            Assert.ReferenceEquals(controller.Distribute(), ar); //The user is sent back to the distribute page
        }
        /// <summary>
        /// Attempts to put unregistered package as barcode
        /// </summary>
        [TestMethod]
        public void Ui_DistributePackage_UnregisteredPackageInput()
        {
            PackageStatusController controller = SetupPackageStatus();
            ActionResult ar = controller.Distribute("90", "19");
            Assert.AreEqual("The barcode you have entered doesn't exist/has not been registered", controller.ViewBag.Error);
            Assert.ReferenceEquals(controller.Distribute(), ar); //The user is sent back to the distribute page
        }
        /// <summary>
        /// Attempts to put long integer as barcode
        /// </summary>
        [TestMethod]
        public void Ui_DistributePackage_LongIntegerInput()
        {
            PackageStatusController controller = SetupPackageStatus();
            ActionResult ar = controller.Distribute("124426427224526245266", "19");
            Assert.AreEqual("Please enter a valid barcode", controller.ViewBag.Error);
            Assert.ReferenceEquals(controller.Distribute(), ar); //The user is sent back to the distribute page
        }
        /*************************************
         * Helper method to initializing class
         * ***********************************/
        /// <summary>
        /// Initialize Package Status Controller
        /// </summary>
        /// <returns>Mock User</returns>
        private PackageStatusController SetupPackageStatus()
        {
            var controller = new PackageStatusController()
            {
                GetUserId = () => "3240a097-a1ca-400a-b88b-0847a35e7aad"
            };
            return controller;
        }

        /// <summary>
        /// A helper function designed to mock a logged in user
        /// </summary>
        /// <returns>A controller with the user mock</returns>
        private PackageController SetupUser()
        {
            var controller = new PackageController()
            {
                GetUserId = () => "3240a097-a1ca-400a-b88b-0847a35e7aad"
            };
            return controller;
        }

    }
}
