using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ENET_Care.Controllers;
using System.Web;
using Moq;
using System.Security.Principal;
using System.Security.Claims;
using System.Web.Mvc;

namespace ENET_Care.Tests
{
    [TestClass]
    public class UiTest
    {
        [TestMethod]
        public void Ui_SendPackage_BadBarcode()
        {
            PackageController controller = SetupUser();
            controller.Send("notanumber", 1);
            Assert.AreEqual("Please enter a valid barcode", controller.ViewBag.Error);

        }

        [TestMethod]
        public void Ui_RegisterPackage_PastExpiryDate()
        {
            PackageController controller = SetupUser();
            ActionResult ar = controller.Register(1, "01/01/2000");
            Assert.AreEqual("The package is expired", controller.ViewBag.Error); 
            Assert.ReferenceEquals(controller.Register(), ar); //The user is sent back to the registration page
        }

        private PackageController SetupUser(){
            var controller = new PackageController()
            {
                GetUserId = () => "3240a097-a1ca-400a-b88b-0847a35e7aad"
            };
            return controller;
        }
    }
}
