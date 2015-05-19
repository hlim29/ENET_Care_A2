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
        /// <summary>
        /// Originally          :agent@enetcare.org
        /// Testing changed to  :agent@enetcare.com
        ///     
        /// Should always in original state ".org"
        /// WARNING: Re-run test with orinial state after changing database
        /// 
        /// NOTE: Should add test method for invalid email address
        /// </summary>
        [TestMethod]
        public void UserTest_UpdateEmail_EmailUpdated()
        {
            string newEmail = "agent@enetcare.org";
            string id = "3240a097-a1ca-400a-b88b-0847a35e7aad";
            UserLogic.UpdateEmail(id, newEmail);
            AspNetUser user = UserLogic.GetUserById(id);
            Assert.AreEqual(newEmail, user.Email);
        }
        /// <summary>
        /// Originally          :centreId = 3
        /// Testing changed to  :centreId = 1
        ///     
        /// Should always in original state "centreId = 3"
        /// WARNING: Re-run test with orinial state after changing database
        /// </summary>
        [TestMethod]
        public void UserTest_UpdateCentreId_CentreIdUpdated()
        {
            int newCentreId = 3;
            string id = "3240a097-a1ca-400a-b88b-0847a35e7aad";
            UserLogic.UpdateCentreId(id, newCentreId);
            AspNetUser user = UserLogic.GetUserById(id);
            Assert.AreEqual(newCentreId, user.CentreId);
        }
    }
}
