using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ENET_Care.BusinessLogic;
using ENET_Care.Data;

namespace ENET_Care.Tests
{
    [TestClass]
    public class PackageSatusTest
    {
        [TestMethod]
        public void PackageStatusTest_ReceivePackage_DataUpdated()
        {
            PackageStatusLogic.ReceivePackage(2, "35cd70ce-35f4-4cb9-8de2-262208cdfe55");
            int centreId = (int)PackageStatusLogic.GetPackageStatusById(7).DestinationCentreID;
            int staffCentreId = (int)UserLogic.GetUserById("35cd70ce-35f4-4cb9-8de2-262208cdfe55").CentreId;

            Assert.AreEqual(centreId, staffCentreId);
        }
    }
}
