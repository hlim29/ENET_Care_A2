using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ENET_Care.BusinessLogic;
using ENET_Care.Data;
using System.Diagnostics;

namespace ENET_Care.Tests
{
    [TestClass]
    public class PackageTest
    {
        [TestMethod]
        public void PackageTest_ExpirationDateTest_Past()
        {
            DateTime pastDate = new DateTime(2000, 01, 01, 00, 00, 00);
            PackageLogic.Result result = PackageLogic.ValidateInput(pastDate);
            Assert.AreEqual(PackageLogic.Result.PastDate, result);
        }

        [TestMethod]
        public void PackageTest_ExpirationDateTest_Future()
        {
            DateTime futureDate = new DateTime(2100,01,01);
            PackageLogic.Result result = PackageLogic.ValidateInput(futureDate);
            Assert.AreEqual(PackageLogic.Result.Ok, result);
        }

        [TestMethod]
        public void PackageTest_ExpirationDateTest_CurrentDate()
        {
            DateTime errorDate = DateTime.Now;
            PackageLogic.Result result = PackageLogic.ValidateInput(errorDate);
            Assert.AreEqual(PackageLogic.Result.PastDate, result);
        }
        [TestMethod]
        public void PackageTest_DiscardNonExistantPackage()
        {
            PackageStatusLogic.DiscardPackage(100000, "y783156");
        }
    }
}
