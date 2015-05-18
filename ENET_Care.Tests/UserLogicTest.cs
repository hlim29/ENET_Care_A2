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
            AspNetUser user = UserLogic.GetUserById("3240a097-a1ca-400a-b88b-0847a35e7aad");
            Debug.WriteLine(user.Email);
            Assert.AreNotEqual(null, user);
        }
        [TestMethod]
        public void UserTest_UpdateUserName_UserNameUpdated()
        {
            string id = "3240a097-a1ca-400a-b88b-0847a35e7aad";
            UserLogic.UpdateUserName(id, "Agent", "EnetCare");
            AspNetUser user = UserLogic.GetUserById(id);
            Assert.AreEqual("Agent", user.FirstName);
        }
    }
}
