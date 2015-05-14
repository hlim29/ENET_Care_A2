using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ENET_Care.BusinessLogic;
using ENET_Care.Data;
using System.Diagnostics;

namespace ENET_Care.Tests
{
    [TestClass]
    public class UserLogicTest
    {
        [TestMethod]
        public void UserTest_GetUserById_getFirstUser()
        {
            AspNetUser user = UserLogic.GetUserById("35cd70ce-35f4-4cb9-8de2-262208cdfe55");
            Debug.WriteLine(user.Email);
            Assert.AreNotEqual(null, user);
        }
    }
}
