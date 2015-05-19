using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ENET_Care.BusinessLogic;
using System.Diagnostics;
using ENET_Care.Data;

namespace ENET_Care.Controllers
{
    public class PagesController : Controller
    {
        // GET: Pages
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Receive()
        {
            return View();
        }

        public ActionResult Send()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Stocktaking()
        {
            return View();
        }

        public ActionResult Discard()
        {
            return View();
        }

        
        public ActionResult MyInformation()
        {
            AspNetUser currentUser = UserLogic.GetUserById(User.Identity.GetUserId());
            SetUpDistCentreDropDown();
            return View(currentUser);
        }
        /// <summary>
        /// Retrieve user input
        /// Update database using BusinessLogic method
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="centreId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MyInformation(string firstName, string lastName, string email, string centreId)
        {
            Debug.WriteLine(firstName);
            string userId = User.Identity.GetUserId();
            UserLogic.UpdateUserName(userId, firstName, lastName);
            UserLogic.UpdateEmail(userId, email);
            UserLogic.UpdateCentreId(userId,Int32.Parse(centreId));

            SetUpDistCentreDropDown();
            AspNetUser currentUser = UserLogic.GetUserById(userId);
            return View(currentUser);
        }
        /// <summary>
        /// Retrieve all DistCentres from database and put it in SelectListItem(Datatype for Razor DropDownList)
        /// </summary>
        private void SetUpDistCentreDropDown()
        {
            using (var context = new Entities())
            {
                ViewBag.DistCentres = (from d in context.DistCentres
                                       select new SelectListItem { Value = d.CentreId.ToString(), Text = d.CentreName }).ToList();
            }
        }
        public ActionResult Distribute()
        {
            return View();
        }
    }
}