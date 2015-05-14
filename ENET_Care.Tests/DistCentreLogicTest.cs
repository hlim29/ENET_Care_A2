using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ENET_Care.BusinessLogic;
using ENET_Care.Data;
using System.Diagnostics;

namespace ENET_Care.Tests
{
    [TestClass]
    public class DistCentreLogicTest
    {
        /// <summary>
        /// Currently its hardcoded to 3
        /// </summary>
        [TestMethod]
        public void DistCentreLogicTest_GetAllDistCentre_CountEqualsThree()
        {
            int totalDistCentre = DistCentreLogic.GetAllDistCentre().Count;
            Assert.Equals(3,totalDistCentre);
        }
    }
}
